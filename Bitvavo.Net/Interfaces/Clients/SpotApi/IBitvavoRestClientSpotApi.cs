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
    }
}
