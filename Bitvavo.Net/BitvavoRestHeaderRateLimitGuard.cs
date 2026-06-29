using System.Net.Http.Headers;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Interfaces;

namespace Bitvavo.Net
{
    /// <summary>
    /// Tracks Bitvavo's REST rate-limit response headers.
    /// </summary>
    internal class BitvavoRestHeaderRateLimitGuard : IRateLimitGuard
    {
        private const string HeaderRateLimitRemaining = "bitvavo-ratelimit-remaining";
        private const string HeaderRateLimitResetAt = "bitvavo-ratelimit-resetat";
        private const string HeaderRateLimitLimit = "bitvavo-ratelimit-limit";

        private static readonly TimeSpan _period = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan _windowBuffer = TimeSpan.FromMilliseconds(250);
        private readonly object _sync = new();
        private readonly Dictionary<string, HeaderLimitState> _states = [];

        /// <inheritdoc />
        public string Name => "BitvavoRestHeaderRateLimitGuard";

        /// <inheritdoc />
        public string Description => "Limit based on Bitvavo REST rate-limit response headers";

        /// <inheritdoc />
        public LimitCheck Check(RateLimitItemType type, RequestDefinition definition, string? apiKey, int requestWeight, string? keySuffix)
        {
            if (type != RateLimitItemType.Request)
                return LimitCheck.NotApplicable;

            var key = GetKey(definition, apiKey);
            lock (_sync)
            {
                if (!_states.TryGetValue(key, out var state))
                    return LimitCheck.NotApplicable;

                var now = DateTime.UtcNow;
                if (state.ResetAt <= now)
                {
                    _states.Remove(key);
                    return LimitCheck.NotApplicable;
                }

                if (state.Remaining >= requestWeight)
                    return LimitCheck.NotApplicable;

                return LimitCheck.Needed(state.ResetAt - now + _windowBuffer, state.Limit, _period, state.Limit - state.Remaining);
            }
        }

        /// <inheritdoc />
        /// <remarks>
        /// We only track the budget locally between responses; we never return <see cref="RateLimitState.Applied"/>,
        /// so this guard does not emit separate <c>RateLimitUpdated</c> usage events. Concurrent in-flight requests
        /// may decrement past the server-authoritative state and brief out-of-order <see cref="Update"/> calls can
        /// rewind it; both self-correct on the next response.
        /// </remarks>
        public RateLimitState ApplyWeight(RateLimitItemType type, RequestDefinition definition, string? apiKey, int requestWeight, string? keySuffix)
        {
            if (type != RateLimitItemType.Request)
                return RateLimitState.NotApplied;

            var key = GetKey(definition, apiKey);
            lock (_sync)
            {
                if (!_states.TryGetValue(key, out var state) || state.ResetAt <= DateTime.UtcNow)
                    return RateLimitState.NotApplied;

                state.Remaining = Math.Max(0, state.Remaining - requestWeight);
            }

            return RateLimitState.NotApplied;
        }

        /// <inheritdoc />
        public void Reset(RateLimitItemType type, RequestDefinition definition, string? apiKey, string? keySuffix, int? amount)
        {
            var key = GetKey(definition, apiKey);
            lock (_sync)
                _states.Remove(key);
        }

        public void Update(RequestDefinition definition, string host, string? apiKey, HttpResponseHeaders responseHeaders)
        {
            if (!TryReadIntHeader(responseHeaders, HeaderRateLimitRemaining, out var remaining)
                || !TryReadResetAt(responseHeaders, out var resetAt))
            {
                return;
            }

            if (!TryReadIntHeader(responseHeaders, HeaderRateLimitLimit, out var limit))
                limit = 1000;

            var key = GetKey(definition, host, apiKey);
            lock (_sync)
                _states[key] = new HeaderLimitState { Limit = limit, Remaining = remaining, ResetAt = resetAt };
        }

        // Bitvavo tracks signed traffic by API key and unsigned public traffic by IP. This client only signs
        // requests whose definitions are authenticated, so public endpoint headers belong to the host bucket.
        private static string GetKey(RequestDefinition definition, string? apiKey)
            => GetKey(definition, definition.BaseAddress, apiKey);

        private static string GetKey(RequestDefinition definition, string host, string? apiKey)
            => definition.Authenticated && !string.IsNullOrEmpty(apiKey)
                ? $"api-key:{apiKey}"
                : $"host:{NormalizeHost(host)}";

        private static string NormalizeHost(string host)
        {
            if (Uri.TryCreate(host, UriKind.Absolute, out var uri))
                return uri.GetLeftPart(UriPartial.Authority);

            return host.TrimEnd('/').ToLowerInvariant();
        }

        private static bool TryReadIntHeader(HttpResponseHeaders responseHeaders, string name, out int value)
        {
            value = default;
            return responseHeaders.TryGetValues(name, out var values)
                && int.TryParse(values.FirstOrDefault(), out value);
        }

        private static bool TryReadResetAt(HttpResponseHeaders responseHeaders, out DateTime resetAt)
        {
            resetAt = default;
            if (!responseHeaders.TryGetValues(HeaderRateLimitResetAt, out var values))
                return false;

            var value = values.FirstOrDefault();
            if (value == null || !long.TryParse(value, out var epochMs))
                return false;

            resetAt = DateTimeOffset.FromUnixTimeMilliseconds(epochMs).UtcDateTime;
            return true;
        }

        private record HeaderLimitState
        {
            public int Limit { get; set; }
            public int Remaining { get; set; }
            public DateTime ResetAt { get; set; }
        }
    }
}
