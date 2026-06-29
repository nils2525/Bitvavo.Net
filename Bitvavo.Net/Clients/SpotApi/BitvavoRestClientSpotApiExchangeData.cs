using Bitvavo.Net.Interfaces.Clients.SpotApi;
using Bitvavo.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitvavo.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitvavoRestClientSpotApiExchangeData : IBitvavoRestClientSpotApiExchangeData
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitvavoRestClientSpotApi _baseClient;

        internal BitvavoRestClientSpotApiExchangeData(BitvavoRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public Task<HttpResult<BitvavoServerTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/time", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoServerTime>(request, null, ct);
        }

        #endregion

        #region Get Markets

        /// <inheritdoc />
        public Task<HttpResult<BitvavoMarket[]>> GetMarketsAsync(string? market = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/markets", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoMarket[]>(request, parameters, ct);
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public Task<HttpResult<BitvavoAsset[]>> GetAssetsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/assets", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoAsset[]>(request, parameters, ct);
        }

        #endregion

        #region Get Tickers 24h

        /// <inheritdoc />
        public Task<HttpResult<BitvavoTicker24h[]>> GetTickers24hAsync(CancellationToken ct = default)
        {
            // All-markets variant costs 25 weight points; single-market variant costs 1.
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/ticker/24h", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTicker24h[]>(request, null, ct, 25);
        }

        /// <inheritdoc />
        public Task<HttpResult<BitvavoTicker24h>> GetTicker24hAsync(string market, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/ticker/24h", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTicker24h>(request, parameters, ct);
        }

        #endregion

        #region Get Ticker Prices

        /// <inheritdoc />
        public Task<HttpResult<BitvavoTickerPrice[]>> GetTickerPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/ticker/price", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTickerPrice[]>(request, null, ct);
        }

        /// <inheritdoc />
        public Task<HttpResult<BitvavoTickerPrice>> GetTickerPriceAsync(string market, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/ticker/price", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTickerPrice>(request, parameters, ct);
        }

        #endregion

        #region Get Ticker Books

        /// <inheritdoc />
        public Task<HttpResult<BitvavoTickerBook[]>> GetTickerBooksAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/ticker/book", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTickerBook[]>(request, null, ct);
        }

        /// <inheritdoc />
        public Task<HttpResult<BitvavoTickerBook>> GetTickerBookAsync(string market, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("market", market);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/ticker/book", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTickerBook>(request, parameters, ct);
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public Task<HttpResult<BitvavoPublicTrade[]>> GetRecentTradesAsync(string market, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/v2/{market}/trades", BitvavoExchange.RateLimiter.Rest, 5, false);
            return _baseClient.SendAsync<BitvavoPublicTrade[]>(request, parameters, ct);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public Task<HttpResult<BitvavoOrderBook>> GetOrderBookAsync(string market, int? depth = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("depth", depth);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/v2/{market}/book", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoOrderBook>(request, parameters, ct);
        }

        #endregion
    }
}
