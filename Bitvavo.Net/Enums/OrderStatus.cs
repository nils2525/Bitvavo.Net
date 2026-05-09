using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Enums
{
    /// <summary>
    /// Order status as returned by the Bitvavo order endpoints and account websocket channel.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// ["<c>new</c>"] Order is open on the order book
        /// </summary>
        [Map("new")]
        New,
        /// <summary>
        /// ["<c>awaitingTrigger</c>"] Trigger order is waiting for the trigger condition
        /// </summary>
        [Map("awaitingTrigger")]
        AwaitingTrigger,
        /// <summary>
        /// ["<c>canceled</c>"] Order was canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>expired</c>"] Order expired before being completely filled
        /// </summary>
        [Map("expired")]
        Expired,
        /// <summary>
        /// ["<c>filled</c>"] Order was fully filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// ["<c>partiallyFilled</c>"] Order was partially filled and is still open or canceled
        /// </summary>
        [Map("partiallyFilled")]
        PartiallyFilled
    }
}
