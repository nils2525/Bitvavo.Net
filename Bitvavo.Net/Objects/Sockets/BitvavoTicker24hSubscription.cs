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

        /// <inheritdoc />
        public override string[] SubscriptionIdentifiers { get; }

        public BitvavoTicker24hSubscription(
            ILogger logger,
            string[] markets,
            Action<DateTime, string?, BitvavoTicker24hUpdate> handler) : base(logger, false)
        {
            _markets = markets;
            _handler = handler;

            // ticker24h updates carry no topic, so the route alone would give every market batch the same identity.
            // CryptoExchange.Net would then treat a second batch as already subscribed and never send its markets, while a
            // route-derived identity per batch would force each batch onto its own connection. Identifying the subscription
            // by its markets lets disjoint batches share a connection, serves an identical market set once and keeps
            // partially overlapping batches apart.
            SubscriptionIdentifiers = markets.Select(market => "ticker24h" + market).ToArray();
            MessageRouter = MessageRouter.CreateForEvent<BitvavoTicker24hUpdate>("ticker24h", DoHandleMessage);
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
