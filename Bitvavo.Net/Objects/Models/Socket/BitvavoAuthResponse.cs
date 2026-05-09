using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// Response from the Bitvavo websocket <c>authenticate</c> action. On success the server replies
    /// with <c>{"event":"authenticate","authenticated":true}</c>; on failure
    /// <c>{"action":"authenticate","errorCode":...,"error":"..."}</c>.
    /// </summary>
    internal record BitvavoAuthResponse
    {
        /// <summary>
        /// ["<c>event</c>"] Set to <c>authenticate</c> on success
        /// </summary>
        [JsonPropertyName("event")]
        public string? Event { get; set; }
        /// <summary>
        /// ["<c>action</c>"] Set to <c>authenticate</c> on action-scoped errors
        /// </summary>
        [JsonPropertyName("action")]
        public string? Action { get; set; }
        /// <summary>
        /// ["<c>authenticated</c>"] True on successful auth
        /// </summary>
        [JsonPropertyName("authenticated")]
        public bool? Authenticated { get; set; }
        /// <summary>
        /// ["<c>errorCode</c>"] Numeric error code on failure
        /// </summary>
        [JsonPropertyName("errorCode")]
        public int? ErrorCode { get; set; }
        /// <summary>
        /// ["<c>error</c>"] Human-readable error message on failure
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
