using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;

namespace Bitvavo.Net.Objects.Sockets
{
    /// <summary>
    /// Subscription for the Bitvavo public <c>book</c> websocket channel. Emits incremental
    /// order book deltas; size <c>0</c> on a level indicates removal.
    /// </summary>
    internal class BitvavoBookSubscription : Subscription
    {
        private readonly Action<DateTime, string?, BitvavoOrderBookUpdate> _handler;
        private readonly string _market;

        public BitvavoBookSubscription(
            ILogger logger,
            string market,
            Action<DateTime, string?, BitvavoOrderBookUpdate> handler) : base(logger, false)
        {
            _market = market;
            _handler = handler;

            MessageRouter = MessageRouter.CreateWithTopicFilter<BitvavoOrderBookUpdate>("book", market, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
            => new BitvavoQuery(new BitvavoSocketRequest
            {
                Action = "subscribe",
                Channels =
                [
                    new BitvavoSubscriptionChannel { Name = "book", Markets = [_market] }
                ]
            }, Authenticated);

        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new BitvavoQuery(new BitvavoSocketRequest
            {
                Action = "unsubscribe",
                Channels =
                [
                    new BitvavoSubscriptionChannel { Name = "book", Markets = [_market] }
                ]
            }, Authenticated);

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitvavoOrderBookUpdate message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
