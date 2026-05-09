using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Account balance entry as returned by Bitvavo GET /v2/balance.
    /// </summary>
    public record BitvavoBalance
    {
        /// <summary>
        /// ["<c>symbol</c>"] Asset short name (for example <c>BTC</c>)
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Balance available for trading and withdrawal
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>inOrder</c>"] Balance reserved in active orders
        /// </summary>
        [JsonPropertyName("inOrder")]
        public decimal InOrder { get; set; }
    }
}
