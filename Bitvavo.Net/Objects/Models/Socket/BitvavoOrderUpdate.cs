using Bitvavo.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order update event received from the Bitvavo <c>account</c> websocket subscription.
    /// </summary>
    public record BitvavoOrderUpdate
    {
        /// <summary>
        /// ["<c>event</c>"] Event identifier, always <c>order</c>
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Bitvavo order identifier (UUID)
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order identifier (UUID), only present when supplied on the order
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>market</c>"] Market name (for example <c>BTC-EUR</c>)
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>created</c>"] Order creation timestamp
        /// </summary>
        [JsonPropertyName("created")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Created { get; set; }
        /// <summary>
        /// ["<c>updated</c>"] Last update timestamp
        /// </summary>
        [JsonPropertyName("updated")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Updated { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Order quantity in base asset
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// ["<c>amountRemaining</c>"] Remaining quantity in base asset
        /// </summary>
        [JsonPropertyName("amountRemaining")]
        public decimal? AmountRemaining { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Limit price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>amountQuote</c>"] Order quantity in quote asset
        /// </summary>
        [JsonPropertyName("amountQuote")]
        public decimal? AmountQuote { get; set; }
        /// <summary>
        /// ["<c>amountQuoteRemaining</c>"] Remaining quote-currency quantity
        /// </summary>
        [JsonPropertyName("amountQuoteRemaining")]
        public decimal? AmountQuoteRemaining { get; set; }
        /// <summary>
        /// ["<c>onHold</c>"] Quantity reserved on hold during processing
        /// </summary>
        [JsonPropertyName("onHold")]
        public decimal? OnHold { get; set; }
        /// <summary>
        /// ["<c>onHoldCurrency</c>"] Asset of the held amount
        /// </summary>
        [JsonPropertyName("onHoldCurrency")]
        public string? OnHoldCurrency { get; set; }
        /// <summary>
        /// ["<c>filledAmount</c>"] Total filled quantity in base asset
        /// </summary>
        [JsonPropertyName("filledAmount")]
        public decimal FilledAmount { get; set; }
        /// <summary>
        /// ["<c>filledAmountQuote</c>"] Total filled value in quote asset
        /// </summary>
        [JsonPropertyName("filledAmountQuote")]
        public decimal FilledAmountQuote { get; set; }
        /// <summary>
        /// ["<c>feePaid</c>"] Total fee paid for this order
        /// </summary>
        [JsonPropertyName("feePaid")]
        public decimal FeePaid { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string? FeeCurrency { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>postOnly</c>"] Whether the order is restricted to the maker side
        /// </summary>
        [JsonPropertyName("postOnly")]
        public bool? PostOnly { get; set; }
    }
}
