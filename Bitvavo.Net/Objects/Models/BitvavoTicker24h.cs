using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// 24h rolling ticker statistics as returned by Bitvavo GET /v2/ticker/24h
    /// and the websocket <c>ticker24h</c> channel.
    /// </summary>
    public record BitvavoTicker24h
    {
        /// <summary>
        /// ["<c>market</c>"] Market name (for example <c>BTC-EUR</c>)
        /// </summary>
        [JsonPropertyName("market")]
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>open</c>"] Open price for the rolling 24h window
        /// </summary>
        [JsonPropertyName("open")]
        public decimal? Open { get; set; }
        /// <summary>
        /// ["<c>high</c>"] High price for the rolling 24h window
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? High { get; set; }
        /// <summary>
        /// ["<c>low</c>"] Low price for the rolling 24h window
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? Low { get; set; }
        /// <summary>
        /// ["<c>last</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal? Last { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume in base asset over the rolling 24h window
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// ["<c>volumeQuote</c>"] Volume in quote asset over the rolling 24h window
        /// </summary>
        [JsonPropertyName("volumeQuote")]
        public decimal? VolumeQuote { get; set; }
        /// <summary>
        /// ["<c>bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal? Bid { get; set; }
        /// <summary>
        /// ["<c>bidSize</c>"] Best bid size
        /// </summary>
        [JsonPropertyName("bidSize")]
        public decimal? BidSize { get; set; }
        /// <summary>
        /// ["<c>ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal? Ask { get; set; }
        /// <summary>
        /// ["<c>askSize</c>"] Best ask size
        /// </summary>
        [JsonPropertyName("askSize")]
        public decimal? AskSize { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp of the snapshot
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
