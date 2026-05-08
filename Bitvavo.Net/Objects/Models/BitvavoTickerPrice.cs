using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Last price for a market as returned by Bitvavo GET /v2/ticker/price
    /// </summary>
    public record BitvavoTickerPrice
    {
        /// <summary>
        /// ["<c>market</c>"] Market name (for example <c>BTC-EUR</c>)
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
    }
}
