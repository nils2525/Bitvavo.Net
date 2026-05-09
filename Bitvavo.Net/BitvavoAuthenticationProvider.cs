using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Bitvavo.Net
{
    /// <summary>
    /// HMAC-SHA256 authentication for the Bitvavo REST API.
    /// <para>
    /// The signature input concatenates the millisecond-timestamp, HTTP method (uppercase),
    /// the path (including <c>/v2</c> prefix and any query string) and the JSON-encoded body
    /// (empty string for GET requests). The result is the lowercase hex HMAC-SHA256 of that
    /// string with the API secret as key. See <see href="https://docs.bitvavo.com/" />.
    /// </para>
    /// </summary>
    internal class BitvavoAuthenticationProvider : AuthenticationProvider<BitvavoCredentials, BitvavoCredentials>
    {
        private static IStringMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitvavoExchange._serializerContext));

        #region Constructors
        public BitvavoAuthenticationProvider(BitvavoCredentials credentials) : base(credentials, credentials)
        {
        }
        #endregion

        #region Methods
        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration requestConfig)
        {
            if (!requestConfig.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);

            var queryString = requestConfig.GetQueryString(false);
            var pathWithQuery = string.IsNullOrEmpty(queryString) ? requestConfig.Path : requestConfig.Path + "?" + queryString;

            var bodyContent = string.Empty;
            if (requestConfig.BodyParameters?.Any() == true)
            {
                if (requestConfig.BodyFormat is RequestBodyFormat.Json)
                    bodyContent = _serializer.Serialize(requestConfig.BodyParameters);
                else if (requestConfig.BodyFormat is RequestBodyFormat.FormData)
                    bodyContent = requestConfig.BodyParameters.ToFormData();

                requestConfig.SetBodyContent(bodyContent);
            }

            var signString = timestamp + requestConfig.Method.Method.ToUpperInvariant() + pathWithQuery + bodyContent;
            var signature = SignHMACSHA256(signString, SignOutputType.Hex);

            requestConfig.Headers ??= new Dictionary<string, string>();
            requestConfig.Headers["Bitvavo-Access-Key"] = Credential.Key;
            requestConfig.Headers["Bitvavo-Access-Signature"] = signature;
            requestConfig.Headers["Bitvavo-Access-Timestamp"] = timestamp;
            requestConfig.Headers["Bitvavo-Access-Window"] = "10000";
        }

        /// <summary>
        /// Compute the HMAC-SHA256 signature for the given input using the configured API secret.
        /// Used by the websocket <c>authenticate</c> action.
        /// </summary>
        internal string Sign(string data) => SignHMACSHA256(data, SignOutputType.Hex);

        /// <summary>
        /// Get the current millisecond timestamp string used in signatures, accounting for the
        /// configured time offset of the supplied socket client.
        /// </summary>
        internal string GetMillisecondsTimestamp(SocketApiClient apiClient) => GetMillisecondTimestamp(apiClient);
        #endregion
    }
}
