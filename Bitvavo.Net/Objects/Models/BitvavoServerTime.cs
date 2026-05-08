using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Server time response (Bitvavo GET /v2/time)
    /// </summary>
    public record BitvavoServerTime
    {
        /// <summary>
        /// ["<c>time</c>"] Current server timestamp
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; set; }
    }
}
