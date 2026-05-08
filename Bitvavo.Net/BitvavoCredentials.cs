using CryptoExchange.Net.Authentication;

namespace Bitvavo.Net
{
    /// <summary>
    /// Bitvavo API credentials
    /// </summary>
    public class BitvavoCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials
        /// </summary>
        public BitvavoCredentials() { }

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="credential">HMAC Credentials</param>
        public BitvavoCredentials(HMACCredential credential) : base(credential.Key, credential.Secret)
        {
        }

        /// <summary>
        /// Create new credentials providing credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public BitvavoCredentials(string key, string secret) : base(key, secret)
        {
        }

        /// <summary>
        /// Specify the HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public BitvavoCredentials WithHMAC(string key, string secret)
        {
            if (!string.IsNullOrEmpty(Key)) throw new InvalidOperationException("Credentials already set");

            Key = key;
            Secret = secret;
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new BitvavoCredentials(this);
    }
}
