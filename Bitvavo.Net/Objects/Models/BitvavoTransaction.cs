using Bitvavo.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Single entry from Bitvavo GET /v2/account/history. The set of populated currency / amount
    /// pairs varies by <see cref="Type"/>: trade entries (<c>buy</c>, <c>sell</c>) populate price/sent/received/fee;
    /// deposit / withdrawal entries omit the price + sent fields. Address is masked for fiat
    /// (e.g. <c>DE32***31</c>) and may be null for trades.
    /// <see href="https://docs.bitvavo.com/docs/rest-api/get-transaction-history/" />.
    /// </summary>
    public record BitvavoTransaction
    {
        /// <summary>
        /// ["<c>transactionId</c>"] Bitvavo-internal transaction id (UUID).
        /// </summary>
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>executedAt</c>"] ISO-8601 UTC timestamp when the transaction was executed.
        /// </summary>
        [JsonPropertyName("executedAt")]
        public DateTime ExecutedAt { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Transaction type (trade direction or transfer category).
        /// </summary>
        [JsonPropertyName("type")]
        public BitvavoTransactionType Type { get; set; }
        /// <summary>
        /// ["<c>priceCurrency</c>"] Quote currency for trade entries; absent for deposit/withdrawal.
        /// </summary>
        [JsonPropertyName("priceCurrency")]
        public string? PriceCurrency { get; set; }
        /// <summary>
        /// ["<c>priceAmount</c>"] Unit price expressed in <see cref="PriceCurrency"/>; absent for deposit/withdrawal.
        /// </summary>
        [JsonPropertyName("priceAmount")]
        public decimal? PriceAmount { get; set; }
        /// <summary>
        /// ["<c>sentCurrency</c>"] Currency of the leg sent away from the user; absent for deposit.
        /// </summary>
        [JsonPropertyName("sentCurrency")]
        public string? SentCurrency { get; set; }
        /// <summary>
        /// ["<c>sentAmount</c>"] Amount sent away from the user; absent for deposit.
        /// </summary>
        [JsonPropertyName("sentAmount")]
        public decimal? SentAmount { get; set; }
        /// <summary>
        /// ["<c>receivedCurrency</c>"] Currency of the leg received by the user; absent for withdrawal.
        /// </summary>
        [JsonPropertyName("receivedCurrency")]
        public string? ReceivedCurrency { get; set; }
        /// <summary>
        /// ["<c>receivedAmount</c>"] Amount received by the user; absent for withdrawal.
        /// </summary>
        [JsonPropertyName("receivedAmount")]
        public decimal? ReceivedAmount { get; set; }
        /// <summary>
        /// ["<c>feesCurrency</c>"] Currency the fee was charged in.
        /// </summary>
        [JsonPropertyName("feesCurrency")]
        public string? FeesCurrency { get; set; }
        /// <summary>
        /// ["<c>feesAmount</c>"] Fee amount charged.
        /// </summary>
        [JsonPropertyName("feesAmount")]
        public decimal? FeesAmount { get; set; }
        /// <summary>
        /// ["<c>address</c>"] On-chain address (digital deposit/withdrawal) or masked bank-account
        /// reference (fiat deposit/withdrawal); null for trades.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
    }
}
