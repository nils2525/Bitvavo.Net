using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// 24h ticker update envelope from the Bitvavo <c>ticker24h</c> websocket subscription.
    /// The exchange batches updates for one or more markets in <see cref="Data"/>.
    /// </summary>
    public record BitvavoTicker24hUpdate
    {
        /// <summary>
        /// ["<c>event</c>"] Event identifier, always <c>ticker24h</c>
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>data</c>"] One or more <see cref="BitvavoTicker24h"/> entries, one per affected market
        /// </summary>
        [JsonPropertyName("data")]
        public BitvavoTicker24h[] Data { get; set; } = Array.Empty<BitvavoTicker24h>();
    }
}
