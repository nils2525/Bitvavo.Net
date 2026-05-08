using System.Net.WebSockets;
using Bitvavo.Net.Clients.MessageHandlers;
using Bitvavo.Net.Interfaces.Clients.ExchangeSocketApi;
using Bitvavo.Net.Objects.Models;
using Bitvavo.Net.Objects.Models.Socket;
using Bitvavo.Net.Objects.Options;
using Bitvavo.Net.Objects.Sockets;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace Bitvavo.Net.Clients.ExchangeSocketApi
{
    /// <inheritdoc cref="IBitvavoSocketClientExchangeApi" />
    internal partial class BitvavoSocketClientExchangeApi : SocketApiClient<BitvavoEnvironment, BitvavoAuthenticationProvider, BitvavoCredentials>, IBitvavoSocketClientExchangeApi
    {
        #region fields
        /// <inheritdoc />
        public new BitvavoSocketOptions ClientOptions => (BitvavoSocketOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping => BitvavoErrors.RestErrorMapping;
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of BitvavoSocketClientExchangeApi
        /// </summary>
        internal BitvavoSocketClientExchangeApi(ILogger logger, BitvavoSocketOptions options)
            : base(logger, options.Environment.SocketExchangeBaseAddress, options, options.ExchangeOptions)
        {
            RateLimiter = BitvavoExchange.RateLimiter.Socket;
        }
        #endregion

        #region Methods
        protected override IMessageSerializer CreateSerializer()
            => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitvavoExchange._serializerContext));

        protected override BitvavoAuthenticationProvider CreateAuthenticationProvider(BitvavoCredentials credentials)
            => new BitvavoAuthenticationProvider(credentials);

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType)
            => new BitvavoSocketMessageHandler();

        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => BitvavoExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
        #endregion

        #region Subscriptions
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string market, Action<DataEvent<BitvavoTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BitvavoTradeUpdate>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<BitvavoTradeUpdate>(BitvavoExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Market)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new BitvavoTradeSubscription(_logger, market, internalHandler);
            return SubscribeAsync(BaseAddress, subscription, ct);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTicker24hUpdatesAsync(string[] markets, Action<DataEvent<BitvavoTicker24hUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BitvavoTicker24hUpdate>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BitvavoTicker24hUpdate>(BitvavoExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new BitvavoTicker24hSubscription(_logger, markets, internalHandler);
            return SubscribeAsync(BaseAddress, subscription, ct);
        }
        #endregion
    }
}
