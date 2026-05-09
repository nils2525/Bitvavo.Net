using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace Bitvavo.Net.Objects.Sockets
{
    /// <summary>
    /// Connection-level <c>authenticate</c> query for the Bitvavo Exchange Socket API.
    /// <para>
    /// Bitvavo's authentication response does NOT include a <c>requestId</c>. Routing is therefore
    /// performed by the <c>event</c>/<c>action</c> field. See
    /// <a href="https://docs.bitvavo.com/docs/authentication-ws/" />.
    /// </para>
    /// </summary>
    internal class BitvavoAuthQuery : Query<BitvavoAuthResponse>
    {
        public BitvavoAuthQuery(BitvavoAuthRequest request) : base(request, false, 1)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitvavoAuthResponse>(
                ["authenticate"], HandleMessage);
        }

        public CallResult<BitvavoAuthResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitvavoAuthResponse message)
        {
            if (message.ErrorCode.HasValue || message.Authenticated == false)
            {
                var code = message.ErrorCode?.ToString() ?? "0";
                var info = BitvavoErrors.RestErrorMapping.GetErrorInfo(code, message.Error ?? "Authentication failed");
                return new CallResult<BitvavoAuthResponse>(new ServerError(code, info));
            }

            return new CallResult<BitvavoAuthResponse>(message, originalData, null);
        }
    }
}
