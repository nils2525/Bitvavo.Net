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
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string market, Action<DataEvent<BitvavoTradeUpdate>> onMessage, CancellationToken ct = default);

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
        Task<CallResult<UpdateSubscription>> SubscribeToTicker24hUpdatesAsync(string[] markets, Action<DataEvent<BitvavoTicker24hUpdate>> onMessage, CancellationToken ct = default);
    }
}
