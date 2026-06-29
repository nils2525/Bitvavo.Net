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
using CryptoExchange.Net.Sockets.Default;
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
        internal BitvavoSocketClientExchangeApi(ILoggerFactory? loggerFactory, BitvavoSocketOptions options)
            : base(loggerFactory, BitvavoExchange.ExchangeName, options.Environment.SocketExchangeBaseAddress, options, options.ExchangeOptions)
        {
            RateLimiter = BitvavoExchange.RateLimiter.ExchangeSocket;
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

        #region Authentication
        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
        {
            if (AuthenticationProvider == null)
                return Task.FromResult<Query?>(null);

            // Bitvavo websocket auth: HMAC-SHA256 of `{timestamp}GET/v2/websocket` keyed by the API
            // secret, encoded as lowercase hex. Sent as a single `authenticate` action; the server
            // authorizes the entire connection until disconnect.
            // Docs: https://docs.bitvavo.com/docs/authentication-ws/
            var timestamp = long.Parse(((BitvavoAuthenticationProvider)AuthenticationProvider).GetMillisecondsTimestamp(this));
            var signString = timestamp + "GET/v2/websocket";
            var signature = ((BitvavoAuthenticationProvider)AuthenticationProvider).Sign(signString);

            var request = new BitvavoAuthRequest
            {
                Action = "authenticate",
                Key = AuthenticationProvider.Key,
                Signature = signature,
                Timestamp = timestamp,
                Window = 10000
            };

            return Task.FromResult<Query?>(new BitvavoAuthQuery(request));
        }
        #endregion

        #region Subscriptions
        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string market, Action<DataEvent<BitvavoTradeUpdate>> onMessage, CancellationToken ct = default)
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

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToTicker24hUpdatesAsync(string[] markets, Action<DataEvent<BitvavoTicker24hUpdate>> onMessage, CancellationToken ct = default)
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
            return SubscribeAsync(subscription, ct);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string market, Action<DataEvent<BitvavoOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, BitvavoOrderBookUpdate>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BitvavoOrderBookUpdate>(BitvavoExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Market)
                    );
            });

            var subscription = new BitvavoBookSubscription(_logger, market, internalHandler);
            return SubscribeAsync(subscription, ct);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string[] markets, Action<DataEvent<BitvavoOrderUpdate>> onOrderUpdate, Action<DataEvent<BitvavoFillUpdate>> onFillUpdate, CancellationToken ct = default)
        {
            var (orderHandler, fillHandler) = BuildAccountHandlers(onOrderUpdate, onFillUpdate);
            var subscription = new BitvavoAccountSubscription(_logger, markets, orderHandler, fillHandler);
            return SubscribeAsync(subscription, ct);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<BitvavoOrderUpdate>> onOrderUpdate, Action<DataEvent<BitvavoFillUpdate>> onFillUpdate, CancellationToken ct = default)
        {
            var (orderHandler, fillHandler) = BuildAccountHandlers(onOrderUpdate, onFillUpdate);
            var subscription = BitvavoAccountSubscription.CreateWildcard(_logger, orderHandler, fillHandler);
            return SubscribeAsync(subscription, ct);
        }

        private (Action<DateTime, string?, BitvavoOrderUpdate> OrderHandler, Action<DateTime, string?, BitvavoFillUpdate> FillHandler) BuildAccountHandlers(
            Action<DataEvent<BitvavoOrderUpdate>> onOrderUpdate,
            Action<DataEvent<BitvavoFillUpdate>> onFillUpdate)
        {
            var orderHandler = new Action<DateTime, string?, BitvavoOrderUpdate>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Updated);
                onOrderUpdate(
                    new DataEvent<BitvavoOrderUpdate>(BitvavoExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Market)
                        .WithDataTimestamp(data.Updated, GetTimeOffset())
                    );
            });

            var fillHandler = new Action<DateTime, string?, BitvavoFillUpdate>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);
                onFillUpdate(
                    new DataEvent<BitvavoFillUpdate>(BitvavoExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Market)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                    );
            });

            return (orderHandler, fillHandler);
        }
        #endregion
    }
}
