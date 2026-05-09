using Bitvavo.Net.Objects.Models;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// Incremental order book update received from the Bitvavo <c>book</c> websocket subscription.
    /// Each <see cref="BitvavoOrderBookEntry"/> with quantity <c>0</c> represents the removal of that
    /// price level. The first event after a subscribe is also a delta - to obtain a full book,
    /// fetch a snapshot via <c>GET /v2/{market}/book</c> and then apply incremental updates whose
    /// <see cref="Nonce"/> is greater than the snapshot nonce.
    /// </summary>
    public record BitvavoOrderBookUpdate
    {
        /// <summary>
        /// ["<c>event</c>"] Event identifier, always <c>book</c>
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
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
        /// ["<c>bids</c>"] Bid changes; size 0 means remove level
        /// </summary>
        [JsonPropertyName("bids")]
        public BitvavoOrderBookEntry[] Bids { get; set; } = Array.Empty<BitvavoOrderBookEntry>();
        /// <summary>
        /// ["<c>asks</c>"] Ask changes; size 0 means remove level
        /// </summary>
        [JsonPropertyName("asks")]
        public BitvavoOrderBookEntry[] Asks { get; set; } = Array.Empty<BitvavoOrderBookEntry>();
        /// <summary>
        /// ["<c>timestamp</c>"] Update timestamp in nanoseconds
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
