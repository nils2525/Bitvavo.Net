using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Single price level inside a Bitvavo order book payload (REST or websocket).
    /// Bitvavo serializes levels as <c>[price, size]</c> arrays; size <c>0</c> indicates
    /// the level should be removed.
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<BitvavoOrderBookEntry>))]
    public record BitvavoOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// Price
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity at this price level (0 indicates the level is removed)
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
