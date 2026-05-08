using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models.Socket
{
    /// <summary>
    /// Response payload returned by the Bitvavo websocket server to a subscribe/unsubscribe request.
    /// <para>
    /// Bitvavo replies with one of two shapes:<br />
    /// 1) Success:   <c>{"event":"subscribed","subscriptions":{"trades":["BTC-EUR"]}}</c>  (or <c>"unsubscribed"</c> for an unsubscribe).<br />
    /// 2) Failure:   <c>{"action":"subscribe","errorCode":205,"error":"market parameter UNK-NOWN is invalid."}</c>.<br />
    /// Both shapes are deserialized into this single record so the originating <see cref="Bitvavo.Net.Objects.Sockets.BitvavoQuery"/>
    /// can decide whether the request succeeded.
    /// </para>
    /// </summary>
    internal record BitvavoSubscriptionResponse
    {
        /// <summary>
        /// ["<c>event</c>"] Set to <c>subscribed</c> or <c>unsubscribed</c> on success, absent on errors.
        /// </summary>
        [JsonPropertyName("event")]
        public string? Event { get; set; }

        /// <summary>
        /// ["<c>action</c>"] Set to <c>subscribe</c> or <c>unsubscribe</c> on action-scoped errors, absent on success.
        /// </summary>
        [JsonPropertyName("action")]
        public string? Action { get; set; }

        /// <summary>
        /// ["<c>errorCode</c>"] Numeric error code (only present on failure).
        /// </summary>
        [JsonPropertyName("errorCode")]
        public int? ErrorCode { get; set; }

        /// <summary>
        /// ["<c>error</c>"] Human readable error message (only present on failure).
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
