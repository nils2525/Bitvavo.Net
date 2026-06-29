using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;

namespace Bitvavo.Net.Objects.Sockets
{
    /// <summary>
    /// Subscription for the Bitvavo authenticated <c>account</c> websocket channel. The channel
    /// emits two distinct event types - <c>order</c> and <c>fill</c> - each carrying a
    /// <c>market</c> field. In per-market mode the <c>market</c> field is used as the topic filter
    /// so a single subscription can serve multiple markets routed to the right per-market handler.
    /// In wildcard mode (markets = <c>"*"</c>) routing happens purely by event type because events
    /// arrive for every currently listed market and a per-market filter would not match anything.
    /// </summary>
    internal class BitvavoAccountSubscription : Subscription
    {
        #region Statics
        /// <summary>
        /// Sentinel used inside <see cref="_markets"/> to indicate that the subscription should be
        /// sent to Bitvavo as the wildcard <c>"markets": "*"</c>.
        /// </summary>
        private const string WildcardMarket = "*";
        #endregion

        #region Fields
        private readonly Action<DateTime, string?, BitvavoOrderUpdate> _orderHandler;
        private readonly Action<DateTime, string?, BitvavoFillUpdate> _fillHandler;
        private readonly string[] _markets;
        #endregion

        #region Constructors
        public BitvavoAccountSubscription(
            ILogger logger,
            string[] markets,
            Action<DateTime, string?, BitvavoOrderUpdate> orderHandler,
            Action<DateTime, string?, BitvavoFillUpdate> fillHandler) : base(logger, true)
        {
            _markets = markets;
            _orderHandler = orderHandler;
            _fillHandler = fillHandler;

            var routes = new List<MessageRoute>();
            foreach (var market in markets)
            {
                routes.Add(MessageRoute.CreateForEvent<BitvavoOrderUpdate>("order", market, DoHandleOrderMessage));
                routes.Add(MessageRoute.CreateForEvent<BitvavoFillUpdate>("fill", market, DoHandleFillMessage));
            }
            MessageRouter = MessageRouter.Create(routes.ToArray());
        }

        private BitvavoAccountSubscription(
            ILogger logger,
            Action<DateTime, string?, BitvavoOrderUpdate> orderHandler,
            Action<DateTime, string?, BitvavoFillUpdate> fillHandler,
            bool wildcard) : base(logger, true)
        {
            _markets = new[] { WildcardMarket };
            _orderHandler = orderHandler;
            _fillHandler = fillHandler;

            // Wildcard subscribe means events arrive for any market; route purely by event type.
            MessageRouter = MessageRouter.Create(
                MessageRoute.CreateForEvent<BitvavoOrderUpdate>("order", DoHandleOrderMessage),
                MessageRoute.CreateForEvent<BitvavoFillUpdate>("fill", DoHandleFillMessage));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Build a subscription that listens to order and fill events for every currently listed
        /// Bitvavo market via the documented <c>"markets": "*"</c> wildcard.
        /// Docs: <a href="https://docs.bitvavo.com/docs/websocket-api/track-your-orders/" />
        /// </summary>
        public static BitvavoAccountSubscription CreateWildcard(
            ILogger logger,
            Action<DateTime, string?, BitvavoOrderUpdate> orderHandler,
            Action<DateTime, string?, BitvavoFillUpdate> fillHandler)
            => new(logger, orderHandler, fillHandler, wildcard: true);

        protected override Query? GetSubQuery(SocketConnection connection)
            => new BitvavoQuery(new BitvavoSocketRequest
            {
                Action = "subscribe",
                Channels =
                [
                    new BitvavoSubscriptionChannel { Name = "account", Markets = _markets }
                ]
            }, Authenticated);

        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new BitvavoQuery(new BitvavoSocketRequest
            {
                Action = "unsubscribe",
                Channels =
                [
                    new BitvavoSubscriptionChannel { Name = "account", Markets = _markets }
                ]
            }, Authenticated);

        public CallResult DoHandleOrderMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitvavoOrderUpdate message)
        {
            _orderHandler.Invoke(receiveTime, originalData, message);
            return CallResult.Ok();
        }

        public CallResult DoHandleFillMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitvavoFillUpdate message)
        {
            _fillHandler.Invoke(receiveTime, originalData, message);
            return CallResult.Ok();
        }
        #endregion
    }
}
