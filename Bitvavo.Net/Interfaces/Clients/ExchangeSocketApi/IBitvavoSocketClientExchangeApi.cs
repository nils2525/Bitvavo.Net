using Bitvavo.Net.Objects.Models;
using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;

namespace Bitvavo.Net.Interfaces.Clients.ExchangeSocketApi
{
    /// <summary>
    /// Bitvavo Exchange Socket API streams (<c>wss://ws.bitvavo.com/v2/</c>).
    /// </summary>
    public interface IBitvavoSocketClientExchangeApi : ISocketApiClient<BitvavoCredentials>, IDisposable
    {
        /// <summary>
        /// Subscribe to the public trade stream for a market.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/websocket-api/trades-subscription/" /><br />
        /// Channel: <c>trades</c>
        /// </para>
        /// </summary>
        /// <param name="market">Market (for example <c>BTC-EUR</c>)</param>
        /// <param name="onMessage">Trade event handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string market, Action<DataEvent<BitvavoTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to the rolling 24h ticker stream for one or more markets.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/websocket-api/ticker-24-hour-subscription/" /><br />
        /// Channel: <c>ticker24h</c>
        /// </para>
        /// </summary>
        /// <param name="markets">Markets to subscribe to (for example <c>BTC-EUR</c>)</param>
        /// <param name="onMessage">Ticker event handler. The exchange may batch multiple markets into a single update.</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTicker24hUpdatesAsync(string[] markets, Action<DataEvent<BitvavoTicker24hUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates for a market. Emits deltas; size 0 indicates
        /// the level is removed. Combine with <c>GET /v2/{market}/book</c> to maintain a local book.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/websocket-api/book-subscription/" /><br />
        /// Channel: <c>book</c>
        /// </para>
        /// </summary>
        /// <param name="market">Market (for example <c>BTC-EUR</c>)</param>
        /// <param name="onMessage">Order book update event handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string market, Action<DataEvent<BitvavoOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to the authenticated <c>account</c> channel for one or more markets. Emits both
        /// <see cref="BitvavoOrderUpdate"/> events (status changes) and <see cref="BitvavoFillUpdate"/>
        /// events (executions). Authentication is established at connection level via the
        /// <c>authenticate</c> action; <see cref="ISocketApiClient{TCredentials}.SetApiCredentials" />
        /// must have been called before subscribing.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/websocket-api/track-your-orders/" /><br />
        /// Channel: <c>account</c>
        /// </para>
        /// </summary>
        /// <param name="markets">Markets to subscribe to (for example <c>BTC-EUR</c>)</param>
        /// <param name="onOrderUpdate">Order status update handler</param>
        /// <param name="onFillUpdate">Fill (execution) handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string[] markets, Action<DataEvent<BitvavoOrderUpdate>> onOrderUpdate, Action<DataEvent<BitvavoFillUpdate>> onFillUpdate, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to the authenticated <c>account</c> channel for all currently listed markets via
        /// the documented <c>"markets": "*"</c> wildcard - a single subscription covers every market.
        /// Emits both <see cref="BitvavoOrderUpdate"/> events (status changes) and
        /// <see cref="BitvavoFillUpdate"/> events (executions); the <c>market</c> field on each event
        /// identifies which market it belongs to. Authentication is established at connection level
        /// via the <c>authenticate</c> action; <see cref="ISocketApiClient{TCredentials}.SetApiCredentials" />
        /// must have been called before subscribing.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/websocket-api/track-your-orders/" /><br />
        /// Channel: <c>account</c>
        /// </para>
        /// </summary>
        /// <param name="onOrderUpdate">Order status update handler</param>
        /// <param name="onFillUpdate">Fill (execution) handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<BitvavoOrderUpdate>> onOrderUpdate, Action<DataEvent<BitvavoFillUpdate>> onFillUpdate, CancellationToken ct = default);
    }
}
