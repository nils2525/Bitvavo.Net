using Bitvavo.Net.Objects;
using CryptoExchange.Net.Objects;

namespace Bitvavo.Net
{
    /// <summary>
    /// Bitvavo environments
    /// </summary>
    public class BitvavoEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Rest client address
        /// </summary>
        public string RestBaseAddress { get; }

        /// <summary>
        /// Exchange Socket API address (<c>wss://ws.bitvavo.com/v2/</c>)
        /// </summary>
        public string SocketExchangeBaseAddress { get; }

        /// <summary>
        /// Market Data Pro Socket API address (<c>wss://ws-mdpro.bitvavo.com/v2/</c>)
        /// </summary>
        public string SocketMarketDataProBaseAddress { get; }

        internal BitvavoEnvironment(string name,
            string restBaseAddress,
            string socketExchangeBaseAddress,
            string socketMarketDataProBaseAddress) : base(name)
        {
            RestBaseAddress = restBaseAddress;
            SocketExchangeBaseAddress = socketExchangeBaseAddress;
            SocketMarketDataProBaseAddress = socketMarketDataProBaseAddress;
        }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public BitvavoEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Bitvavo environment by name
        /// </summary>
        public static BitvavoEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static BitvavoEnvironment Live { get; }
            = new BitvavoEnvironment(TradeEnvironmentNames.Live,
                                     BitvavoApiAddresses.Default.RestClientAddress,
                                     BitvavoApiAddresses.Default.SocketExchangeClientAddress,
                                     BitvavoApiAddresses.Default.SocketMarketDataProClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static BitvavoEnvironment CreateCustom(
                        string name,
                        string restAddress,
                        string socketExchangeAddress,
                        string socketMarketDataProAddress)
            => new BitvavoEnvironment(name, restAddress, socketExchangeAddress, socketMarketDataProAddress);
    }
}
