using Bitvavo.Net.Converters;
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
        /// ["<c>markets</c>"] Markets to subscribe to (for example <c>BTC-EUR</c>). A single-element
        /// array containing <c>"*"</c> serializes as the bare string <c>"*"</c>, which Bitvavo
        /// interprets as "every currently listed market" - see
        /// <a href="https://docs.bitvavo.com/docs/websocket-api/track-your-orders/" />.
        /// </summary>
        [JsonPropertyName("markets")]
        [JsonConverter(typeof(BitvavoMarketsConverter))]
        public string[] Markets { get; set; } = Array.Empty<string>();
    }
}
