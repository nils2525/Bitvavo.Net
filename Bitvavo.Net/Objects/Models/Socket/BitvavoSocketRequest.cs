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
        /// ["<c>requestId</c>"] Client-supplied id; Bitvavo echoes it back in both the success
        /// (<c>{"event":"subscribed","requestId":...}</c>) and error
        /// (<c>{"action":"subscribe","requestId":...,"errorCode":...}</c>) response. Used by the
        /// framework's routing layer to match a response back to its originating query when several
        /// requests share a single websocket connection.
        /// </summary>
        [JsonPropertyName("requestId")]
        public long RequestId { get; set; }

        /// <summary>
        /// ["<c>channels</c>"] Channels to subscribe or unsubscribe from.
        /// </summary>
        [JsonPropertyName("channels")]
        public BitvavoSubscriptionChannel[] Channels { get; set; } = Array.Empty<BitvavoSubscriptionChannel>();
    }
}
