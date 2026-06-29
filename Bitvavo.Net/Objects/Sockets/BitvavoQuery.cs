using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net;
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
    /// <item>a success event - <c>{"event":"subscribed","requestId":N,...}</c> or <c>{"event":"unsubscribed","requestId":N,...}</c>; or</item>
    /// <item>an action-scoped error - <c>{"action":"subscribe","requestId":N,"errorCode":...,"error":"..."}</c>.</item>
    /// </list>
    /// Both shapes echo the client-supplied <c>requestId</c>; this query assigns a fresh id on
    /// construction and routes by it, so concurrent subscribes on a single connection are
    /// disambiguated. The ack is consumed silently on success and surfaced as a
    /// <see cref="ServerError"/> on failure.
    /// </para>
    /// </summary>
    internal class BitvavoQuery : Query<BitvavoSubscriptionResponse>
    {
        public BitvavoQuery(BitvavoSocketRequest request, bool authenticated, int weight = 1) : base(AssignRequestId(request), authenticated, weight)
        {
            MessageRouter = MessageRouter.CreateForQuery<BitvavoSubscriptionResponse>(
                request.RequestId.ToString(), HandleMessage);
        }

        public CallResult<BitvavoSubscriptionResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitvavoSubscriptionResponse message)
        {
            if (message.ErrorCode.HasValue)
            {
                var code = message.ErrorCode.Value.ToString();
                var info = BitvavoErrors.RestErrorMapping.GetErrorInfo(code, message.Error);
                return CallResult.Fail<BitvavoSubscriptionResponse>(new ServerError(code, info));
            }

            return CallResult.Ok(message, originalData);
        }

        private static BitvavoSocketRequest AssignRequestId(BitvavoSocketRequest request)
        {
            request.RequestId = ExchangeHelpers.NextId();
            return request;
        }
    }
}
