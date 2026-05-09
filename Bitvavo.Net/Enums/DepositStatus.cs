using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Enums
{
    /// <summary>
    /// Deposit status as returned by Bitvavo /depositHistory for fiat deposits.
    /// Digital asset deposits do not include this field.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositStatus>))]
    public enum DepositStatus
    {
        /// <summary>
        /// ["<c>completed</c>"] Completed
        /// </summary>
        [Map("completed")]
        Completed,
        /// <summary>
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [Map("canceled")]
        Canceled
    }
}
