using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// ["<c>limit</c>"] Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// ["<c>market</c>"] Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// ["<c>stopLoss</c>"] Stop loss (market) order
        /// </summary>
        [Map("stopLoss")]
        StopLoss,
        /// <summary>
        /// ["<c>stopLossLimit</c>"] Stop loss limit order
        /// </summary>
        [Map("stopLossLimit")]
        StopLossLimit,
        /// <summary>
        /// ["<c>takeProfit</c>"] Take profit (market) order
        /// </summary>
        [Map("takeProfit")]
        TakeProfit,
        /// <summary>
        /// ["<c>takeProfitLimit</c>"] Take profit limit order
        /// </summary>
        [Map("takeProfitLimit")]
        TakeProfitLimit
    }
}
