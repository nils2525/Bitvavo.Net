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
        /// May return <c>null</c> after the migration to tick-size validation; in that case use
        /// <see cref="TickSize"/> for price rounding instead.
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int? PricePrecision { get; set; }
        /// <summary>
        /// ["<c>tickSize</c>"] Smallest price increment. Replaces <c>pricePrecision</c> for markets
        /// that have migrated to tick-size validation.
        /// </summary>
        [JsonPropertyName("tickSize")]
        public decimal? TickSize { get; set; }
        /// <summary>
        /// ["<c>quantityDecimals</c>"] Maximum number of decimal digits accepted for the order quantity
        /// in the base asset.
        /// </summary>
        [JsonPropertyName("quantityDecimals")]
        public int? QuantityDecimals { get; set; }
        /// <summary>
        /// ["<c>notionalDecimals</c>"] Maximum number of decimal digits accepted for the order value
        /// in the quote asset.
        /// </summary>
        [JsonPropertyName("notionalDecimals")]
        public int? NotionalDecimals { get; set; }
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
        /// ["<c>maxOrderInBaseAsset</c>"] Maximum order quantity expressed in the base asset
        /// </summary>
        [JsonPropertyName("maxOrderInBaseAsset")]
        public decimal? MaxOrderInBaseAsset { get; set; }
        /// <summary>
        /// ["<c>maxOrderInQuoteAsset</c>"] Maximum order value expressed in the quote asset
        /// </summary>
        [JsonPropertyName("maxOrderInQuoteAsset")]
        public decimal? MaxOrderInQuoteAsset { get; set; }
        /// <summary>
        /// ["<c>maxOpenOrders</c>"] Maximum number of concurrently open orders allowed for this market.
        /// </summary>
        [JsonPropertyName("maxOpenOrders")]
        public int? MaxOpenOrders { get; set; }
        /// <summary>
        /// ["<c>feeCategory</c>"] Fee category identifier (e.g. <c>A</c>, <c>B</c>, <c>C</c>) used to
        /// look up the maker/taker fee schedule.
        /// </summary>
        [JsonPropertyName("feeCategory")]
        public string? FeeCategory { get; set; }
        /// <summary>
        /// ["<c>orderTypes</c>"] Supported order types for this market
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public string[] OrderTypes { get; set; } = Array.Empty<string>();
    }
}
