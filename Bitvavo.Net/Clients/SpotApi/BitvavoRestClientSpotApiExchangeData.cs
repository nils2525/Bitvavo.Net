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
        public Task<WebCallResult<BitvavoServerTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/time", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoServerTime>(request, null, ct);
        }

        #endregion

        #region Get Markets

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoMarket[]>> GetMarketsAsync(string? market = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("market", market);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/markets", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoMarket[]>(request, parameters, ct);
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoAsset[]>> GetAssetsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/assets", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoAsset[]>(request, parameters, ct);
        }

        #endregion

        #region Get Tickers 24h

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoTicker24h[]>> GetTickers24hAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/ticker/24h", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTicker24h[]>(request, null, ct);
        }

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoTicker24h>> GetTicker24hAsync(string market, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("market", market);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/ticker/24h", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTicker24h>(request, parameters, ct);
        }

        #endregion

        #region Get Ticker Prices

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoTickerPrice[]>> GetTickerPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/ticker/price", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTickerPrice[]>(request, null, ct);
        }

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoTickerPrice>> GetTickerPriceAsync(string market, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("market", market);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/ticker/price", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTickerPrice>(request, parameters, ct);
        }

        #endregion

        #region Get Ticker Books

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoTickerBook[]>> GetTickerBooksAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/ticker/book", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTickerBook[]>(request, null, ct);
        }

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoTickerBook>> GetTickerBookAsync(string market, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("market", market);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/ticker/book", BitvavoExchange.RateLimiter.Rest, 1, false);
            return _baseClient.SendAsync<BitvavoTickerBook>(request, parameters, ct);
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoPublicTrade[]>> GetRecentTradesAsync(string market, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/{market}/trades", BitvavoExchange.RateLimiter.Rest, 5, false);
            return _baseClient.SendAsync<BitvavoPublicTrade[]>(request, parameters, ct);
        }

        #endregion
    }
}
