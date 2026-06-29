using Bitvavo.Net.Enums;
using Bitvavo.Net.Interfaces.Clients.SpotApi;
using Bitvavo.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitvavo.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitvavoRestClientSpotApiTrading : IBitvavoRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitvavoRestClientSpotApi _baseClient;

        internal BitvavoRestClientSpotApiTrading(BitvavoRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Place Limit Order

        /// <inheritdoc />
        public Task<HttpResult<BitvavoOrder>> PlaceLimitOrderAsync(
            string market,
            OrderSide side,
            decimal amount,
            decimal price,
            long operatorId,
            string? clientOrderId = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            string? selfTradePrevention = null,
            bool? responseRequired = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);
            parameters.Add("side", side);
            parameters.Add("orderType", OrderType.Limit);
            parameters.Add("operatorId", operatorId);
            parameters.Add("amount", amount);
            parameters.Add("price", price);
            parameters.Add("clientOrderId", clientOrderId);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("postOnly", postOnly);
            parameters.Add("selfTradePrevention", selfTradePrevention);
            parameters.Add("responseRequired", responseRequired);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v2/order", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoOrder>(request, parameters, ct);
        }

        #endregion

        #region Place Market Order

        /// <inheritdoc />
        public Task<HttpResult<BitvavoOrder>> PlaceMarketOrderAsync(
            string market,
            OrderSide side,
            long operatorId,
            decimal? amount = null,
            decimal? amountQuote = null,
            string? clientOrderId = null,
            bool? responseRequired = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);
            parameters.Add("side", side);
            parameters.Add("orderType", OrderType.Market);
            parameters.Add("operatorId", operatorId);
            parameters.Add("amount", amount);
            parameters.Add("amountQuote", amountQuote);
            parameters.Add("clientOrderId", clientOrderId);
            parameters.Add("responseRequired", responseRequired);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v2/order", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoOrder>(request, parameters, ct);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public Task<HttpResult<BitvavoCancelOrderResult>> CancelOrderAsync(
            string market,
            long operatorId,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(orderId) && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be supplied.");

            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);
            parameters.Add("operatorId", operatorId);
            parameters.Add("orderId", orderId);
            parameters.Add("clientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/v2/order", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoCancelOrderResult>(request, parameters, ct);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public Task<HttpResult<BitvavoOrder>> GetOrderAsync(string market, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(orderId) && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be supplied.");

            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);
            parameters.Add("orderId", orderId);
            parameters.Add("clientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/order", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoOrder>(request, parameters, ct);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public Task<HttpResult<BitvavoOrder[]>> GetOrdersAsync(string market, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, string? orderIdFrom = null, string? orderIdTo = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);
            parameters.Add("limit", limit);
            parameters.Add("start", startTime);
            parameters.Add("end", endTime);
            parameters.Add("orderIdFrom", orderIdFrom);
            parameters.Add("orderIdTo", orderIdTo);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/orders", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoOrder[]>(request, parameters, ct);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public Task<HttpResult<BitvavoOrder[]>> GetOpenOrdersAsync(string? market = null, string? baseAsset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);
            parameters.Add("base", baseAsset);

            // Weight is 25 without market filter, 1 with it. Use the worst-case as the static weight
            // as a per-call override because request definitions are cached by method/path.
            var weight = string.IsNullOrEmpty(market) ? 25 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/ordersOpen", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoOrder[]>(request, parameters, ct, weight);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public Task<HttpResult<BitvavoUserTrade[]>> GetUserTradesAsync(string market, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, string? tradeIdFrom = null, string? tradeIdTo = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);
            parameters.Add("limit", limit);
            parameters.Add("start", startTime);
            parameters.Add("end", endTime);
            parameters.Add("tradeIdFrom", tradeIdFrom);
            parameters.Add("tradeIdTo", tradeIdTo);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/trades", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoUserTrade[]>(request, parameters, ct);
        }

        #endregion
    }
}
