using Bitvavo.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Trading market info as returned by Bitvavo GET /v2/markets
    /// </summary>
    public record BitvavoMarket
    {
        /// <summary>
        /// ["<c>market</c>"] Market name, formatted as <c>BASE-QUOTE</c> (for example <c>BTC-EUR</c>)
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Market trading status
        /// </summary>
        [JsonPropertyName("status")]
        public MarketStatus Status { get; set; }
        /// <summary>
        /// ["<c>base</c>"] Base asset symbol
        /// </summary>
        [JsonPropertyName("base")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote</c>"] Quote asset symbol
        /// </summary>
        [JsonPropertyName("quote")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>pricePrecision</c>"] Number of significant digits used for the price.
        /// May return <c>null</c> after the migration to tick-size validation.
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int? PricePrecision { get; set; }
        /// <summary>
        /// ["<c>minOrderInBaseAsset</c>"] Minimum order quantity expressed in the base asset
        /// </summary>
        [JsonPropertyName("minOrderInBaseAsset")]
        public decimal MinOrderInBaseAsset { get; set; }
        /// <summary>
        /// ["<c>minOrderInQuoteAsset</c>"] Minimum order value expressed in the quote asset
        /// </summary>
        [JsonPropertyName("minOrderInQuoteAsset")]
        public decimal MinOrderInQuoteAsset { get; set; }
        /// <summary>
        /// ["<c>orderTypes</c>"] Supported order types for this market
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public string[] OrderTypes { get; set; } = Array.Empty<string>();
    }
}
