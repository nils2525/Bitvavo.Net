using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Single fill (trade execution) inside a Bitvavo order response.
    /// </summary>
    public record BitvavoOrderFill
    {
        /// <summary>
        /// ["<c>id</c>"] Trade id, unique per market
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Trade execution timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Filled quantity in base asset
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Fill price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>taker</c>"] True when the user was the taker
        /// </summary>
        [JsonPropertyName("taker")]
        public bool Taker { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee paid for this fill (negative for rebates)
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal? Fee { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Asset the fee is denominated in
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string? FeeCurrency { get; set; }
        /// <summary>
        /// ["<c>settled</c>"] Whether the fee has settled
        /// </summary>
        [JsonPropertyName("settled")]
        public bool Settled { get; set; }
    }
}
