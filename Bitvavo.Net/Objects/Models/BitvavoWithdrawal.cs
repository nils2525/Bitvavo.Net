using Bitvavo.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Withdrawal history entry returned by Bitvavo GET /v2/withdrawalHistory.
    /// </summary>
    public record BitvavoWithdrawal
    {
        /// <summary>
        /// ["<c>timestamp</c>"] Withdrawal timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Asset short name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Amount sent (excludes fee)
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>address</c>"] Destination wallet address or bank account identifier
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>paymentId</c>"] Optional payment id / memo / destination tag
        /// </summary>
        [JsonPropertyName("paymentId")]
        public string? PaymentId { get; set; }
        /// <summary>
        /// ["<c>txId</c>"] On-chain transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string? TxId { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Withdrawal fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Withdrawal status
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawalStatus Status { get; set; }
    }
}
