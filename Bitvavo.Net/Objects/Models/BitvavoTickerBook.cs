using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Best bid/ask snapshot as returned by Bitvavo GET /v2/ticker/book
    /// </summary>
    public record BitvavoTickerBook
    {
        /// <summary>
        /// ["<c>market</c>"] Market name (for example <c>BTC-EUR</c>)
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal? Bid { get; set; }
        /// <summary>
        /// ["<c>bidSize</c>"] Size at the best bid
        /// </summary>
        [JsonPropertyName("bidSize")]
        public decimal? BidSize { get; set; }
        /// <summary>
        /// ["<c>ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal? Ask { get; set; }
        /// <summary>
        /// ["<c>askSize</c>"] Size at the best ask
        /// </summary>
        [JsonPropertyName("askSize")]
        public decimal? AskSize { get; set; }
    }
}
