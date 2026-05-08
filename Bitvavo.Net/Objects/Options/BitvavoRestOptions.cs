using CryptoExchange.Net.Objects.Options;

namespace Bitvavo.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitvavoRestClient
    /// </summary>
    public class BitvavoRestOptions : RestExchangeOptions<BitvavoEnvironment, BitvavoCredentials>
    {
        /// <summary>
        /// Default options for the BitvavoRestClient
        /// </summary>
        internal static BitvavoRestOptions Default { get; set; } = new BitvavoRestOptions
        {
            Environment = BitvavoEnvironment.Live,
            AutoTimestamp = false
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BitvavoRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Options for the Exchange API
        /// </summary>
        public RestApiOptions ApiOptions { get; private set; } = new RestApiOptions();

        internal BitvavoRestOptions Set(BitvavoRestOptions targetOptions)
        {
            targetOptions = base.Set<BitvavoRestOptions>(targetOptions);
            targetOptions.ApiOptions = ApiOptions.Set(targetOptions.ApiOptions);
            return targetOptions;
        }
    }
}
