using System.Net.WebSockets;
using Bitvavo.Net.Clients.MessageHandlers;
using Bitvavo.Net.Interfaces.Clients.MarketDataProSocketApi;
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

namespace Bitvavo.Net.Clients.MarketDataProSocketApi
{
    /// <inheritdoc cref="IBitvavoSocketClientMarketDataProApi" />
    internal partial class BitvavoSocketClientMarketDataProApi : SocketApiClient<BitvavoEnvironment, BitvavoAuthenticationProvider, BitvavoCredentials>, IBitvavoSocketClientMarketDataProApi
    {
        #region fields
        /// <inheritdoc />
        public new BitvavoSocketOptions ClientOptions => (BitvavoSocketOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping => BitvavoErrors.RestErrorMapping;
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of BitvavoSocketClientMarketDataProApi
        /// </summary>
        internal BitvavoSocketClientMarketDataProApi(ILogger logger, BitvavoSocketOptions options)
            : base(logger, options.Environment.SocketMarketDataProBaseAddress, options, options.MarketDataProOptions)
        {
            RateLimiter = BitvavoExchange.RateLimiter.MarketDataProSocket;
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
            return SubscribeAsync(subscription, ct);
        }
        #endregion
    }
}
