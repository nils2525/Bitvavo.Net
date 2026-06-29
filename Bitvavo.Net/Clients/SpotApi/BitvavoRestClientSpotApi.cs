using Bitvavo.Net.Clients.MessageHandlers;
using Bitvavo.Net.Interfaces.Clients.SpotApi;
using Bitvavo.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;

namespace Bitvavo.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBitvavoRestClientSpotApi" />
    internal partial class BitvavoRestClientSpotApi : RestApiClient<BitvavoEnvironment, BitvavoAuthenticationProvider, BitvavoCredentials>, IBitvavoRestClientSpotApi
    {
        #region fields
        /// <inheritdoc />
        public new BitvavoRestOptions ClientOptions => (BitvavoRestOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping { get; } = BitvavoErrors.RestErrorMapping;

        protected override IRestMessageHandler MessageHandler { get; } = new BitvavoRestMessageHandler(BitvavoErrors.RestErrorMapping);
        #endregion

        /// <inheritdoc />
        public string ExchangeName => "Bitvavo";

        /// <inheritdoc />
        public IBitvavoRestClientSpotApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IBitvavoRestClientSpotApiAccount Account { get; }

        /// <inheritdoc />
        public IBitvavoRestClientSpotApiTrading Trading { get; }

        public BitvavoRestClientSpotApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, BitvavoRestOptions options) :
            base(loggerFactory, BitvavoExchange.ExchangeName, httpClient, options.Environment.RestBaseAddress, options, options.ApiOptions)
        {
            RequestBodyFormat = RequestBodyFormat.Json;
            RequestBodyEmptyContent = string.Empty;

            ExchangeData = new BitvavoRestClientSpotApiExchangeData(this);
            Account = new BitvavoRestClientSpotApiAccount(this);
            Trading = new BitvavoRestClientSpotApiTrading(this);
        }

        protected override IMessageSerializer CreateSerializer()
            => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitvavoExchange._serializerContext));

        protected override BitvavoAuthenticationProvider CreateAuthenticationProvider(BitvavoCredentials credentials)
            => new BitvavoAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BitvavoExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        internal Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => base.SendAsync<T>(definition, parameters, cancellationToken, null, weight);

        protected override async Task<HttpResult<T>> GetResponseAsync2<T>(RequestDefinition requestDefinition, IRequest request, IRateLimitGate? gate, CancellationToken cancellationToken)
        {
            var result = await base.GetResponseAsync2<T>(requestDefinition, request, gate, cancellationToken).ConfigureAwait(false);
            if (gate == BitvavoExchange.RateLimiter.Rest && result.ResponseHeaders != null)
                BitvavoExchange.RateLimiter.UpdateRestRateLimitFromHeaders(requestDefinition, request.Uri.GetLeftPart(UriPartial.Authority), GetAuthenticationProvider()?.Key, result.ResponseHeaders);

            return result;
        }
    }
}
