using Bitvavo.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Interfaces.Clients;

namespace Bitvavo.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Bitvavo REST API.
    /// </summary>
    public interface IBitvavoRestClient : IRestClient<BitvavoCredentials>
    {
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IBitvavoRestClientSpotApi"/>
        IBitvavoRestClientSpotApi SpotApi { get; }
    }
}
