using Bitvavo.Net.Clients.ExchangeSocketApi;
using Bitvavo.Net.Clients.MarketDataProSocketApi;
using Bitvavo.Net.Interfaces.Clients;
using Bitvavo.Net.Interfaces.Clients.ExchangeSocketApi;
using Bitvavo.Net.Interfaces.Clients.MarketDataProSocketApi;
using Bitvavo.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bitvavo.Net.Clients
{
    /// <inheritdoc cref="IBitvavoSocketClient" />
    public class BitvavoSocketClient : BaseSocketClient<BitvavoEnvironment, BitvavoCredentials>, IBitvavoSocketClient
    {
        /// <inheritdoc />
        public IBitvavoSocketClientExchangeApi ExchangeApi { get; }

        /// <inheritdoc />
        public IBitvavoSocketClientMarketDataProApi MarketDataProApi { get; }

        #region ctor

        /// <summary>
        /// Create a new instance of the BitvavoSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitvavoSocketClient(Action<BitvavoSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of the BitvavoSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public BitvavoSocketClient(IOptions<BitvavoSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Bitvavo")
        {
            Initialize(options.Value);

            ExchangeApi = AddApiClient(new BitvavoSocketClientExchangeApi(_logger, options.Value));
            MarketDataProApi = AddApiClient(new BitvavoSocketClientMarketDataProApi(_logger, options.Value));
        }
        #endregion

        /// <inheritdoc />
        public override void SetApiCredentials(BitvavoCredentials credentials)
        {
            ExchangeApi.SetApiCredentials(credentials);
            MarketDataProApi.SetApiCredentials(credentials);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitvavoSocketOptions> optionsDelegate)
        {
            BitvavoSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }
    }
}
