using Bitvavo.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Authenticated user trade as returned by Bitvavo GET /v2/trades.
    /// </summary>
    public record BitvavoUserTrade
    {
        /// <summary>
        /// ["<c>id</c>"] Trade id, unique per market
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Bitvavo identifier of the order this trade belongs to
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client identifier of the order this trade belongs to
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Trade execution timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>market</c>"] Market name (for example <c>BTC-EUR</c>)
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Trade side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
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
        /// ["<c>fee</c>"] Fee paid (negative for rebates)
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Asset the fee is denominated in
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeCurrency { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>settled</c>"] Whether the fee has settled
        /// </summary>
        [JsonPropertyName("settled")]
        public bool Settled { get; set; }
    }
}
