using Bitvavo.Net.Interfaces.Clients.ExchangeSocketApi;
using Bitvavo.Net.Interfaces.Clients.MarketDataProSocketApi;
using CryptoExchange.Net.Interfaces.Clients;

namespace Bitvavo.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Bitvavo websocket APIs.
    /// <para>
    /// Bitvavo offers two distinct websocket APIs - the regular Exchange Socket API and the
    /// low-latency Market Data Pro Socket API - exposed via <see cref="ExchangeApi"/> and
    /// <see cref="MarketDataProApi"/>.
    /// </para>
    /// </summary>
    public interface IBitvavoSocketClient : ISocketClient<BitvavoCredentials>
    {
        /// <summary>
        /// Exchange Socket API streams (<c>wss://ws.bitvavo.com/v2/</c>)
        /// </summary>
        /// <see cref="IBitvavoSocketClientExchangeApi"/>
        IBitvavoSocketClientExchangeApi ExchangeApi { get; }

        /// <summary>
        /// Market Data Pro Socket API streams (<c>wss://ws-mdpro.bitvavo.com/v2/</c>)
        /// </summary>
        /// <see cref="IBitvavoSocketClientMarketDataProApi"/>
        IBitvavoSocketClientMarketDataProApi MarketDataProApi { get; }
    }
}
