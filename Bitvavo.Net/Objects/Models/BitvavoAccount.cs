using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Account info as returned by Bitvavo GET /v2/account.
    /// </summary>
    public record BitvavoAccount
    {
        /// <summary>
        /// ["<c>fees</c>"] Trading fee tier
        /// </summary>
        [JsonPropertyName("fees")]
        public BitvavoAccountFees Fees { get; set; } = new BitvavoAccountFees();
    }
}
