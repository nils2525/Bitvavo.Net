namespace Bitvavo.Net.Objects
{
    /// <summary>
    /// Api addresses usable for the Bitvavo clients
    /// </summary>
    public class BitvavoApiAddresses
    {
        /// <summary>
        /// The address used by the BitvavoRestClient for the rest API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitvavoSocketClient for the Exchange Socket API
        /// </summary>
        public string SocketExchangeClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitvavoSocketClient for the Market Data Pro Socket API
        /// </summary>
        public string SocketMarketDataProClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Bitvavo API
        /// </summary>
        public static BitvavoApiAddresses Default = new BitvavoApiAddresses
        {
            RestClientAddress = "https://api.bitvavo.com",
            SocketExchangeClientAddress = "wss://ws.bitvavo.com/v2/",
            SocketMarketDataProClientAddress = "wss://ws-mdpro.bitvavo.com/v2/"
        };
    }
}
