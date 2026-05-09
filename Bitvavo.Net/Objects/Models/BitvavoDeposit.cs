using Bitvavo.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Deposit history entry returned by Bitvavo GET /v2/depositHistory. Fiat deposits include
    /// <see cref="Status"/>; digital asset deposits include <see cref="TxId"/>.
    /// </summary>
    public record BitvavoDeposit
    {
        /// <summary>
        /// ["<c>timestamp</c>"] Deposit timestamp
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
        /// ["<c>amount</c>"] Deposit amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>address</c>"] Wallet address (digital) or bank account identifier (fiat)
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// ["<c>paymentId</c>"] Optional payment id / memo / destination tag
        /// </summary>
        [JsonPropertyName("paymentId")]
        public string? PaymentId { get; set; }
        /// <summary>
        /// ["<c>txId</c>"] On-chain transaction id (digital deposits only)
        /// </summary>
        [JsonPropertyName("txId")]
        public string? TxId { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Deposit fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status of the deposit (fiat deposits only)
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus? Status { get; set; }
    }
}
