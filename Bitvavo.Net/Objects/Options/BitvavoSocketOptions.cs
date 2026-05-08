using CryptoExchange.Net.Objects.Options;

namespace Bitvavo.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitvavoSocketClient.
    /// <para>
    /// Bitvavo exposes two distinct websocket APIs - the regular Exchange Socket API and the
    /// low-latency Market Data Pro Socket API. The same socket client connects to both via
    /// independent api-clients, configured through <see cref="ExchangeOptions"/> and
    /// <see cref="MarketDataProOptions"/>.
    /// </para>
    /// </summary>
    public class BitvavoSocketOptions : SocketExchangeOptions<BitvavoEnvironment, BitvavoCredentials>
    {
        /// <summary>
        /// Default options for the BitvavoSocketClient
        /// </summary>
        internal static BitvavoSocketOptions Default { get; set; } = new BitvavoSocketOptions
        {
            Environment = BitvavoEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10,
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BitvavoSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Options for the Exchange Socket API (<c>wss://ws.bitvavo.com/v2/</c>)
        /// </summary>
        public SocketApiOptions ExchangeOptions { get; private set; } = new SocketApiOptions();

        /// <summary>
        /// Options for the Market Data Pro Socket API (<c>wss://ws-mdpro.bitvavo.com/v2/</c>)
        /// </summary>
        public SocketApiOptions MarketDataProOptions { get; private set; } = new SocketApiOptions();

        internal BitvavoSocketOptions Set(BitvavoSocketOptions targetOptions)
        {
            targetOptions = base.Set<BitvavoSocketOptions>(targetOptions);
            targetOptions.ExchangeOptions = ExchangeOptions.Set(targetOptions.ExchangeOptions);
            targetOptions.MarketDataProOptions = MarketDataProOptions.Set(targetOptions.MarketDataProOptions);
            return targetOptions;
        }
    }
}
