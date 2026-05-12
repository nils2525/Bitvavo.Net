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
        public Task<WebCallResult<BitvavoOrder>> PlaceLimitOrderAsync(
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
            var parameters = new ParameterCollection();
            parameters.Add("market", market);
            parameters.AddEnum("side", side);
            parameters.AddEnum("orderType", OrderType.Limit);
            parameters.Add("operatorId", operatorId);
            parameters.AddString("amount", amount);
            parameters.AddString("price", price);
            parameters.AddOptional("clientOrderId", clientOrderId);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptional("postOnly", postOnly);
            parameters.AddOptional("selfTradePrevention", selfTradePrevention);
            parameters.AddOptional("responseRequired", responseRequired);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/order", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoOrder>(request, parameters, ct);
        }

        #endregion

        #region Place Market Order

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoOrder>> PlaceMarketOrderAsync(
            string market,
            OrderSide side,
            long operatorId,
            decimal? amount = null,
            decimal? amountQuote = null,
            string? clientOrderId = null,
            bool? responseRequired = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("market", market);
            parameters.AddEnum("side", side);
            parameters.AddEnum("orderType", OrderType.Market);
            parameters.Add("operatorId", operatorId);
            parameters.AddOptionalString("amount", amount);
            parameters.AddOptionalString("amountQuote", amountQuote);
            parameters.AddOptional("clientOrderId", clientOrderId);
            parameters.AddOptional("responseRequired", responseRequired);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/order", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoOrder>(request, parameters, ct);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoCancelOrderResult>> CancelOrderAsync(
            string market,
            long operatorId,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(orderId) && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be supplied.");

            var parameters = new ParameterCollection();
            parameters.Add("market", market);
            parameters.Add("operatorId", operatorId);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("clientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/v2/order", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoCancelOrderResult>(request, parameters, ct);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoOrder>> GetOrderAsync(string market, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(orderId) && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be supplied.");

            var parameters = new ParameterCollection();
            parameters.Add("market", market);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("clientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/order", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoOrder>(request, parameters, ct);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoOrder[]>> GetOrdersAsync(string market, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, string? orderIdFrom = null, string? orderIdTo = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("market", market);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalMilliseconds("start", startTime);
            parameters.AddOptionalMilliseconds("end", endTime);
            parameters.AddOptional("orderIdFrom", orderIdFrom);
            parameters.AddOptional("orderIdTo", orderIdTo);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/orders", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoOrder[]>(request, parameters, ct);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoOrder[]>> GetOpenOrdersAsync(string? market = null, string? baseAsset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("market", market);
            parameters.AddOptional("base", baseAsset);

            // Weight is 25 without market filter, 1 with it. Use the worst-case as the static weight
            // as a per-call override because request definitions are cached by method/path.
            var weight = string.IsNullOrEmpty(market) ? 25 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/ordersOpen", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoOrder[]>(request, parameters, ct, weight);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoUserTrade[]>> GetUserTradesAsync(string market, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, string? tradeIdFrom = null, string? tradeIdTo = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("market", market);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalMilliseconds("start", startTime);
            parameters.AddOptionalMilliseconds("end", endTime);
            parameters.AddOptional("tradeIdFrom", tradeIdFrom);
            parameters.AddOptional("tradeIdTo", tradeIdTo);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/trades", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoUserTrade[]>(request, parameters, ct);
        }

        #endregion
    }
}
