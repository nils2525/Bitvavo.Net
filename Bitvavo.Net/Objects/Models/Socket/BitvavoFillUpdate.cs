using Bitvavo.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// Fill update event received from the Bitvavo <c>account</c> websocket subscription.
    /// </summary>
    public record BitvavoFillUpdate
    {
        /// <summary>
        /// ["<c>event</c>"] Event identifier, always <c>fill</c>
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Fill timestamp
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
        /// ["<c>orderId</c>"] Bitvavo order identifier (UUID)
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client-supplied order id, when present on the order
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>fillId</c>"] Fill (trade) identifier
        /// </summary>
        [JsonPropertyName("fillId")]
        public string FillId { get; set; } = string.Empty;
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
        /// ["<c>side</c>"] Side of the fill
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>taker</c>"] True when the user was the taker
        /// </summary>
        [JsonPropertyName("taker")]
        public bool Taker { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal? Fee { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string? FeeCurrency { get; set; }
    }
}
