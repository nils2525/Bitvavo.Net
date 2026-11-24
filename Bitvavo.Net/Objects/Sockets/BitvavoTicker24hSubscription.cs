using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;

namespace Bitvavo.Net.Objects.Sockets
{
    /// <summary>
    /// Subscription for the Bitvavo <c>ticker24h</c> websocket channel covering one or more markets.
    /// </summary>
    internal class BitvavoTicker24hSubscription : Subscription
    {
        private readonly Action<DateTime, string?, BitvavoTicker24hUpdate> _handler;
        private readonly string[] _markets;

        public BitvavoTicker24hSubscription(
            ILogger logger,
            string[] markets,
            Action<DateTime, string?, BitvavoTicker24hUpdate> handler) : base(logger, false)
        {
            _markets = markets;
            _handler = handler;

            // CryptoExchange.Net skips the subscribe request of a subscription whose routes an earlier subscription on
            // the same connection already covers. ticker24h updates carry no topic, so every market batch would look
            // like the first one and only that batch would ever be subscribed. The second route is keyed by this batch's
            // market set: it never matches an update, because updates have no topic, but only an identical batch counts
            // as covered.
            var marketSetKey = "markets:" + string.Join(',', markets.Order(StringComparer.Ordinal));
            MessageRouter = MessageRouter.Create(
                MessageRoute.CreateForEvent<BitvavoTicker24hUpdate>("ticker24h", DoHandleMessage),
                MessageRoute.CreateForEvent<BitvavoTicker24hUpdate>("ticker24h", marketSetKey, DoHandleMessage));
        }

        protected override Query? GetSubQuery(SocketConnection connection)
            => new BitvavoQuery(new BitvavoSocketRequest
            {
                Action = "subscribe",
                Channels =
                [
                    new BitvavoSubscriptionChannel { Name = "ticker24h", Markets = _markets }
                ]
            }, Authenticated);

        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new BitvavoQuery(new BitvavoSocketRequest
            {
                Action = "unsubscribe",
                Channels =
                [
                    new BitvavoSubscriptionChannel { Name = "ticker24h", Markets = _markets }
                ]
            }, Authenticated);

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitvavoTicker24hUpdate message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.Ok();
        }
    }
}
