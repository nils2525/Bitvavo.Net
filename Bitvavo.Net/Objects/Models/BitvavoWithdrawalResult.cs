using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Response payload of Bitvavo POST /v2/withdrawal.
    /// </summary>
    public record BitvavoWithdrawalResult
    {
        /// <summary>
        /// ["<c>success</c>"] True when the withdrawal request was accepted
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Asset withdrawn
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Total amount deducted from the account
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}
