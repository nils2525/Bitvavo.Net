using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;

namespace Bitvavo.Net.Interfaces.Clients.MarketDataProSocketApi
{
    /// <summary>
    /// Bitvavo Market Data Pro Socket API streams (<c>wss://ws-mdpro.bitvavo.com/v2/</c>).
    /// <para>
    /// Provides low-latency, full-fidelity market data with sequence-numbered events.
    /// </para>
    /// </summary>
    public interface IBitvavoSocketClientMarketDataProApi : ISocketApiClient<BitvavoCredentials>, IDisposable
    {
        /// <summary>
        /// Subscribe to the public trade stream for a market via Market Data Pro.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/ws-market-data-pro-api/trades-subscription/" /><br />
        /// Channel: <c>trades</c>
        /// </para>
        /// </summary>
        /// <param name="market">Market (for example <c>BTC-EUR</c>)</param>
        /// <param name="onMessage">Trade event handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string market, Action<DataEvent<BitvavoTradeUpdate>> onMessage, CancellationToken ct = default);
    }
}
