using Bitvavo.Net.Converters;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;
using System.Text.Json.Serialization;

namespace Bitvavo.Net
{
    /// <summary>
    /// Bitvavo exchange information and configuration
    /// </summary>
    public static class BitvavoExchange
    {
        internal static JsonSerializerContext _serializerContext = JsonSerializerContextCache.GetOrCreate<BitvavoSourceGenerationContext>();

        /// <summary>
        /// Platform metadata
        /// </summary>
        public static PlatformInfo Metadata { get; } = new PlatformInfo(
                "Bitvavo",
                "Bitvavo",
                "https://raw.githubusercontent.com/JKorf/Bitvavo.Net/master/Bitvavo.Net/Icon/icon.png",
                "https://bitvavo.com",
                ["https://docs.bitvavo.com/"],
                PlatformType.CryptoCurrencyExchange,
                CentralizationType.Centralized
                );

        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Bitvavo";

        /// <summary>
        /// Display name
        /// </summary>
        public static string DisplayName => "Bitvavo";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/Bitvavo.Net/master/Bitvavo.Net/Icon/icon.png";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://bitvavo.com/";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://docs.bitvavo.com/"
            };

        /// <summary>
        /// Type of exchange
        /// </summary>
        public static ExchangeType Type { get; } = ExchangeType.CEX;

        /// <summary>
        /// Aliases for Bitvavo assets
        /// </summary>
        public static AssetAliasConfiguration AssetAliases { get; } = new AssetAliasConfiguration
        {
            Aliases = []
        };

        /// <summary>
        /// Format a base and quote asset to a Bitvavo recognized symbol (<c>BASE-QUOTE</c>)
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            baseAsset = AssetAliases.CommonToExchangeName(baseAsset.ToUpperInvariant());
            quoteAsset = AssetAliases.CommonToExchangeName(quoteAsset.ToUpperInvariant());

            return baseAsset + "-" + quoteAsset;
        }

        /// <summary>
        /// Rate limiter configuration for the Bitvavo API
        /// </summary>
        public static BitvavoRateLimiters RateLimiter { get; } = new BitvavoRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Bitvavo API. All limits are sourced from
    /// <see href="https://docs.bitvavo.com/docs/rate-limits/" />.
    /// </summary>
    public class BitvavoRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

        /// <summary>
        /// Event when the rate limit is updated. Note that it's only updated when a request is send, so there are no specific updates when the current usage is decaying.
        /// </summary>
        public event Action<RateLimitUpdateEvent> RateLimitUpdated;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal BitvavoRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            // REST: 1000 weight points / minute. Tracked per API key when authenticated;
            // when no key is configured the per-key bucket effectively scopes to the process.
            // Exceeding triggers HTTP 429 with errorCode 105 (1-min lockout for keyed users,
            // 15-min for IP-based unauthenticated users).
            Rest = new RateLimitGate("Rest")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKey, new LimitItemTypeFilter(RateLimitItemType.Request), 1000, TimeSpan.FromMinutes(1), RateLimitWindowType.Sliding));

            // Exchange WebSocket API: 5000 messages / second per session (= per connection).
            // Exceeding triggers HTTP 429 with errorCode 112.
            ExchangeSocket = new RateLimitGate("ExchangeSocket")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new LimitItemTypeFilter(RateLimitItemType.Request), 5000, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));

            // Market Data Pro WebSocket API: 50 messages / second per session (= per connection).
            // Exceeding triggers HTTP 429 with errorCode 112.
            MarketDataProSocket = new RateLimitGate("MarketDataProSocket")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new LimitItemTypeFilter(RateLimitItemType.Request), 50, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));

            Rest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            Rest.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            ExchangeSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            ExchangeSocket.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            MarketDataProSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            MarketDataProSocket.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
        }


        internal IRateLimitGate Rest { get; private set; }
        internal IRateLimitGate ExchangeSocket { get; private set; }
        internal IRateLimitGate MarketDataProSocket { get; private set; }
    }
}
