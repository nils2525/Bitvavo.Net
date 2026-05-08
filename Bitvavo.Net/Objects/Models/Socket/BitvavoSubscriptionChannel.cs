using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// Single channel inside a Bitvavo subscribe/unsubscribe request.
    /// </summary>
    internal record BitvavoSubscriptionChannel
    {
        /// <summary>
        /// ["<c>name</c>"] Channel name (for example <c>trades</c>, <c>ticker24h</c>, <c>book</c>).
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>markets</c>"] Markets to subscribe to (for example <c>BTC-EUR</c>).
        /// </summary>
        [JsonPropertyName("markets")]
        public string[] Markets { get; set; } = Array.Empty<string>();
    }
}
