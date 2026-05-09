using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Trading fee tier as returned by Bitvavo GET /v2/account.
    /// </summary>
    public record BitvavoAccountFees
    {
        /// <summary>
        /// ["<c>taker</c>"] Taker fee rate (decimal fraction, for example <c>0.0025</c>)
        /// </summary>
        [JsonPropertyName("taker")]
        public decimal Taker { get; set; }
        /// <summary>
        /// ["<c>maker</c>"] Maker fee rate (decimal fraction, for example <c>0.0015</c>)
        /// </summary>
        [JsonPropertyName("maker")]
        public decimal Maker { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Account 30-day trading volume in EUR
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
    }
}
