using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Enums
{
    /// <summary>
    /// Market trading status as returned by the Bitvavo /markets endpoint.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarketStatus>))]
    public enum MarketStatus
    {
        /// <summary>
        /// ["<c>trading</c>"] Market is trading normally
        /// </summary>
        [Map("trading")]
        Trading,
        /// <summary>
        /// ["<c>halted</c>"] Trading on this market has been halted
        /// </summary>
        [Map("halted")]
        Halted,
        /// <summary>
        /// ["<c>auction</c>"] Market is in auction mode
        /// </summary>
        [Map("auction")]
        Auction
    }
}
