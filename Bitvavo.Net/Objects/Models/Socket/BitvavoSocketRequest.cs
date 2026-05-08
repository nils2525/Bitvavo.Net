using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// Subscribe/unsubscribe request payload for the Bitvavo websocket APIs.
    /// </summary>
    internal record BitvavoSocketRequest
    {
        /// <summary>
        /// ["<c>action</c>"] Action name, either <c>subscribe</c> or <c>unsubscribe</c>.
        /// </summary>
        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>channels</c>"] Channels to subscribe or unsubscribe from.
        /// </summary>
        [JsonPropertyName("channels")]
        public BitvavoSubscriptionChannel[] Channels { get; set; } = Array.Empty<BitvavoSubscriptionChannel>();
    }
}
