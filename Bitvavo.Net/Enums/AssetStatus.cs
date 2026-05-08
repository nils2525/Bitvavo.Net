using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Enums
{
    /// <summary>
    /// Deposit/withdrawal status for a Bitvavo asset.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetStatus>))]
    public enum AssetStatus
    {
        /// <summary>
        /// ["<c>OK</c>"] Deposits/withdrawals are enabled
        /// </summary>
        [Map("OK")]
        Ok,
        /// <summary>
        /// ["<c>MAINTENANCE</c>"] Asset network is under maintenance
        /// </summary>
        [Map("MAINTENANCE")]
        Maintenance,
        /// <summary>
        /// ["<c>DELISTED</c>"] Asset has been delisted
        /// </summary>
        [Map("DELISTED")]
        Delisted
    }
}
