using Bitvavo.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitvavo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitvavo Spot exchange data endpoints. Exchange data includes market data
    /// (tickers, order books, etc.) and system status.
    /// </summary>
    public interface IBitvavoRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Get the current Bitvavo server time.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-server-time/" /><br />
        /// Endpoint:<br />
        /// GET /v2/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoServerTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all trading markets, including their status, base/quote and trade limits.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-markets/" /><br />
        /// Endpoint:<br />
        /// GET /v2/markets
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Optional market filter (for example <c>BTC-EUR</c>)</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoMarket[]>> GetMarketsAsync(string? market = null, CancellationToken ct = default);

        /// <summary>
        /// Get all assets known to the exchange, including supported networks.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-asset-data/" /><br />
        /// Endpoint:<br />
        /// GET /v2/assets
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Optional asset filter (for example <c>BTC</c>)</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoAsset[]>> GetAssetsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get rolling 24h ticker statistics for ALL markets.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-candlestick-data-24-h/" /><br />
        /// Endpoint:<br />
        /// GET /v2/ticker/24h
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoTicker24h[]>> GetTickers24hAsync(CancellationToken ct = default);

        /// <summary>
        /// Get rolling 24h ticker statistics for a single market. Bitvavo returns a SINGLE object
        /// (not an array of one) when the <c>market</c> query parameter is supplied.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-candlestick-data-24-h/" /><br />
        /// Endpoint:<br />
        /// GET /v2/ticker/24h?market={market}
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market (for example <c>BTC-EUR</c>)</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoTicker24h>> GetTicker24hAsync(string market, CancellationToken ct = default);

        /// <summary>
        /// Get the latest price for ALL markets.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-ticker-prices/" /><br />
        /// Endpoint:<br />
        /// GET /v2/ticker/price
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoTickerPrice[]>> GetTickerPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the latest price for a single market. Bitvavo returns a SINGLE object
        /// (not an array of one) when the <c>market</c> query parameter is supplied.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-ticker-prices/" /><br />
        /// Endpoint:<br />
        /// GET /v2/ticker/price?market={market}
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market (for example <c>BTC-EUR</c>)</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoTickerPrice>> GetTickerPriceAsync(string market, CancellationToken ct = default);

        /// <summary>
        /// Get the best bid/ask snapshot for ALL markets.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-ticker-book/" /><br />
        /// Endpoint:<br />
        /// GET /v2/ticker/book
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoTickerBook[]>> GetTickerBooksAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the best bid/ask snapshot for a single market. Bitvavo returns a SINGLE object
        /// (not an array of one) when the <c>market</c> query parameter is supplied.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-ticker-book/" /><br />
        /// Endpoint:<br />
        /// GET /v2/ticker/book?market={market}
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market (for example <c>BTC-EUR</c>)</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoTickerBook>> GetTickerBookAsync(string market, CancellationToken ct = default);

        /// <summary>
        /// Get recent public trades for a market.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-trades/" /><br />
        /// Endpoint:<br />
        /// GET /v2/{market}/trades
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market (for example <c>BTC-EUR</c>)</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoPublicTrade[]>> GetRecentTradesAsync(string market, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get an order book snapshot for a market. The response includes a <c>nonce</c> that can be
        /// used to align the snapshot with subsequent <c>book</c> websocket updates.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-order-book/" /><br />
        /// Endpoint:<br />
        /// GET /v2/{market}/book
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market (for example <c>BTC-EUR</c>)</param>
        /// <param name="depth">["<c>depth</c>"] Number of bids/asks to return (max 1000, default 1000)</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoOrderBook>> GetOrderBookAsync(string market, int? depth = null, CancellationToken ct = default);
    }
}
