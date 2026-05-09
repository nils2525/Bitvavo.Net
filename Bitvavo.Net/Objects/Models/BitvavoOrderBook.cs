using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Order book snapshot as returned by Bitvavo GET /v2/{market}/book.
    /// </summary>
    public record BitvavoOrderBook
    {
        /// <summary>
        /// ["<c>market</c>"] Market name (for example <c>BTC-EUR</c>)
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>nonce</c>"] Sequence number; increments with every order book change
        /// </summary>
        [JsonPropertyName("nonce")]
        public long Nonce { get; set; }
        /// <summary>
        /// ["<c>bids</c>"] Bids as <c>[price, size]</c> pairs
        /// </summary>
        [JsonPropertyName("bids")]
        public BitvavoOrderBookEntry[] Bids { get; set; } = Array.Empty<BitvavoOrderBookEntry>();
        /// <summary>
        /// ["<c>asks</c>"] Asks as <c>[price, size]</c> pairs
        /// </summary>
        [JsonPropertyName("asks")]
        public BitvavoOrderBookEntry[] Asks { get; set; } = Array.Empty<BitvavoOrderBookEntry>();
        /// <summary>
        /// ["<c>timestamp</c>"] Snapshot timestamp in nanoseconds
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
