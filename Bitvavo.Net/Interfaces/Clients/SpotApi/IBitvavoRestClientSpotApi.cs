using CryptoExchange.Net.Interfaces.Clients;

namespace Bitvavo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitvavo Spot API endpoints
    /// </summary>
    public interface IBitvavoRestClientSpotApi : IRestApiClient<BitvavoCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IBitvavoRestClientSpotApiExchangeData" />
        IBitvavoRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to the authenticated account (balances, fees, deposits/withdrawals)
        /// </summary>
        /// <see cref="IBitvavoRestClientSpotApiAccount" />
        IBitvavoRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to placing/cancelling orders and reading user trades
        /// </summary>
        /// <see cref="IBitvavoRestClientSpotApiTrading" />
        IBitvavoRestClientSpotApiTrading Trading { get; }
    }
}
