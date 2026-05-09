using System.Net.Http.Headers;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;

namespace Bitvavo.Net.Clients.MessageHandlers
{
    internal class BitvavoRestMessageHandler : JsonRestMessageHandler
    {
        // Bitvavo response headers documented at https://docs.bitvavo.com/docs/rate-limits/
        private const string HeaderRateLimitResetAt = "bitvavo-ratelimit-resetat";

        private readonly ErrorMapping _errorMapping;
        public override bool RequiresSeekableStream => true;
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitvavoExchange._serializerContext);

        public BitvavoRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        private Error ParseErrorInternal(JsonElement rootElement)
        {
            var code = rootElement.TryGetProperty("errorCode", out var codeProp) ? codeProp.ToString() : "?";

            string? reason = null;
            if (rootElement.TryGetProperty("error", out var errorProp))
                reason = errorProp.GetString();
            reason ??= rootElement.TryGetProperty("message", out var messageProp) ? messageProp.GetString() : null;

            return new ServerError(code, _errorMapping.GetErrorInfo(code, reason));
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            return ParseErrorInternal(document!.RootElement);
        }

        /// <summary>
        /// Bitvavo's rate-limit response carries the docs-published <c>bitvavo-ratelimit-resetat</c>
        /// header (epoch ms). Surface it as the rate-limit error's retry-after so the framework's
        /// <c>RateLimitGate</c> blocks subsequent calls until the server's reset moment instead of
        /// bursting into more 429s. Source: <see href="https://docs.bitvavo.com/docs/rate-limits/" />.
        /// </summary>
        public override async ValueTask<ServerRateLimitError> ParseErrorRateLimitResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            // First try the Bitvavo-specific header.
            if (TryReadResetAt(responseHeaders, out var resetAt))
            {
                // Carry the parsed errorCode/message into the error so the wrapper-side mapper
                // logs `[105] market parameter ... is invalid.` instead of the empty fallback.
                var (_, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
                var message = document?.RootElement.ValueKind == JsonValueKind.Object && document.RootElement.TryGetProperty("error", out var errorProp)
                    ? errorProp.GetString()
                    : null;
                return new ServerRateLimitError(message) { RetryAfter = resetAt };
            }

            // Fall back to the framework's `Retry-After` handling.
            return await base.ParseErrorRateLimitResponse(httpStatusCode, responseHeaders, responseStream).ConfigureAwait(false);
        }

        public override async ValueTask<Error?> CheckForErrorResponse(RequestDefinition request, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            if (document!.RootElement.ValueKind is JsonValueKind.Array)
                return null;

            // Bitvavo error responses always carry an "errorCode" property; success responses for non-array endpoints don't.
            if (document.RootElement.TryGetProperty("errorCode", out _))
                return ParseErrorInternal(document.RootElement);

            return await base.CheckForErrorResponse(request, responseHeaders, responseStream).ConfigureAwait(false);
        }

        private static bool TryReadResetAt(HttpResponseHeaders responseHeaders, out DateTime resetAt)
        {
            resetAt = default;
            var header = responseHeaders.FirstOrDefault(h => h.Key.Equals(HeaderRateLimitResetAt, StringComparison.OrdinalIgnoreCase));
            if (header.Value == null)
                return false;

            var value = header.Value.FirstOrDefault();
            if (value == null || !long.TryParse(value, out var epochMs))
                return false;

            resetAt = DateTimeOffset.FromUnixTimeMilliseconds(epochMs).UtcDateTime;
            return true;
        }
    }
}
