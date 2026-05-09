using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// Authentication request payload for the Bitvavo websocket Exchange API. The signature is the
    /// HMAC-SHA256 of <c>{timestamp}GET/v2/websocket</c> using the API secret as key, encoded as
    /// lowercase hex.
    /// <para>
    /// See <a href="https://docs.bitvavo.com/docs/authentication-ws/" />.
    /// </para>
    /// </summary>
    internal record BitvavoAuthRequest
    {
        /// <summary>
        /// ["<c>action</c>"] Always <c>authenticate</c>.
        /// </summary>
        [JsonPropertyName("action")]
        public string Action { get; set; } = "authenticate";

        /// <summary>
        /// ["<c>key</c>"] API key.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>signature</c>"] HMAC-SHA256 signature.
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>timestamp</c>"] Unix milliseconds timestamp used in the signature.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// ["<c>window</c>"] Validity window in milliseconds (default 10000).
        /// </summary>
        [JsonPropertyName("window")]
        public long Window { get; set; } = 10000;
    }
}
