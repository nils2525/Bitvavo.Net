using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace Bitvavo.Net.Objects.Sockets
{
    /// <summary>
    /// Subscribe/unsubscribe query for the Bitvavo websocket API.
    /// <para>
    /// Bitvavo replies to a subscribe/unsubscribe with either:
    /// <list type="bullet">
    /// <item>a success event - <c>{"event":"subscribed",...}</c> or <c>{"event":"unsubscribed",...}</c>; or</item>
    /// <item>an action-scoped error - <c>{"action":"subscribe","errorCode":...,"error":"..."}</c>.</item>
    /// </list>
    /// We route both shapes into <see cref="BitvavoSubscriptionResponse"/> via the <c>subscribed</c>/<c>unsubscribed</c>/<c>subscribe</c>/<c>unsubscribe</c>
    /// type identifiers so the query consumes them silently and surfaces a <see cref="ServerError"/> when an error code is returned.
    /// </para>
    /// </summary>
    internal class BitvavoQuery : Query<BitvavoSubscriptionResponse>
    {
        public BitvavoQuery(BitvavoSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            // Match every possible response shape the server can produce for a subscribe/unsubscribe action.
            // Per Bitvavo's docs and observed behavior the "topic" we need to match against (channel name) is
            // not present at message-handler time, so we route by type identifier only - one Bitvavo subscribe/unsubscribe
            // is in flight per connection at a time, which makes ambiguity impossible.
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitvavoSubscriptionResponse>(
                ["subscribed", "unsubscribed", "subscribe", "unsubscribe"], HandleMessage);
        }

        public CallResult<BitvavoSubscriptionResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitvavoSubscriptionResponse message)
        {
            if (message.ErrorCode.HasValue)
            {
                var code = message.ErrorCode.Value.ToString();
                var info = BitvavoErrors.RestErrorMapping.GetErrorInfo(code, message.Error);
                return new CallResult<BitvavoSubscriptionResponse>(new ServerError(code, info));
            }

            return new CallResult<BitvavoSubscriptionResponse>(message, originalData, null);
        }
    }
}
