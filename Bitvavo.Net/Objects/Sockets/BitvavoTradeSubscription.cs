using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;

namespace Bitvavo.Net.Objects.Sockets
{
    /// <summary>
    /// Subscription for the Bitvavo public <c>trades</c> websocket channel.
    /// </summary>
    internal class BitvavoTradeSubscription : Subscription
    {
        private readonly Action<DateTime, string?, BitvavoTradeUpdate> _handler;
        private readonly string _market;

        public BitvavoTradeSubscription(
            ILogger logger,
            string market,
            Action<DateTime, string?, BitvavoTradeUpdate> handler) : base(logger, false)
        {
            _market = market;
            _handler = handler;

            MessageRouter = MessageRouter.CreateForEvent<BitvavoTradeUpdate>("trade", market, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
            => new BitvavoQuery(new BitvavoSocketRequest
            {
                Action = "subscribe",
                Channels =
                [
                    new BitvavoSubscriptionChannel { Name = "trades", Markets = [_market] }
                ]
            }, Authenticated);

        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new BitvavoQuery(new BitvavoSocketRequest
            {
                Action = "unsubscribe",
                Channels =
                [
                    new BitvavoSubscriptionChannel { Name = "trades", Markets = [_market] }
                ]
            }, Authenticated);

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitvavoTradeUpdate message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.Ok();
        }
    }
}
