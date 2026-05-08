using Bitvavo.Net.Clients.SpotApi;
using Bitvavo.Net.Interfaces.Clients;
using Bitvavo.Net.Interfaces.Clients.SpotApi;
using Bitvavo.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bitvavo.Net.Clients
{
    /// <inheritdoc cref="IBitvavoRestClient" />
    public class BitvavoRestClient : BaseRestClient<BitvavoEnvironment, BitvavoCredentials>, IBitvavoRestClient
    {
        /// <inheritdoc />
        public IBitvavoRestClientSpotApi SpotApi { get; }

        #region ctor
        /// <summary>
        /// Create a new instance of the BitvavoRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitvavoRestClient(Action<BitvavoRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the BitvavoRestClient
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public BitvavoRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BitvavoRestOptions> options)
            : base(loggerFactory, "Bitvavo")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new BitvavoRestClientSpotApi(_logger, httpClient, options.Value));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitvavoRestOptions> optionsDelegate)
        {
            BitvavoRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
        #endregion
    }
}
