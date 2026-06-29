using Bitvavo.Net.Enums;
using Bitvavo.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitvavo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitvavo Spot trading endpoints (orders and user trades).
    /// </summary>
    public interface IBitvavoRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a limit order.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/create-order/" /><br />
        /// Endpoint:<br />
        /// POST /v2/order
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market name (for example <c>BTC-EUR</c>)</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="amount">["<c>amount</c>"] Quantity in base asset</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="operatorId">["<c>operatorId</c>"] Trader/bot identifier (required by the API)</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Optional client order id (must be a UUID)</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Optional time in force (default GTC)</param>
        /// <param name="postOnly">["<c>postOnly</c>"] Maker-only flag</param>
        /// <param name="selfTradePrevention">["<c>selfTradePrevention</c>"] Optional self-trade prevention strategy</param>
        /// <param name="responseRequired">["<c>responseRequired</c>"] Set to <c>false</c> to skip the response body</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitvavoOrder>> PlaceLimitOrderAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Place a market order. Either <paramref name="amount"/> (base) or <paramref name="amountQuote"/> must be supplied.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/create-order/" /><br />
        /// Endpoint:<br />
        /// POST /v2/order
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market name</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="operatorId">["<c>operatorId</c>"] Trader/bot identifier (required)</param>
        /// <param name="amount">["<c>amount</c>"] Quantity in base asset</param>
        /// <param name="amountQuote">["<c>amountQuote</c>"] Quantity in quote asset</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Optional client order id (UUID)</param>
        /// <param name="responseRequired">["<c>responseRequired</c>"] Set to <c>false</c> to skip the response body</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitvavoOrder>> PlaceMarketOrderAsync(
            string market,
            OrderSide side,
            long operatorId,
            decimal? amount = null,
            decimal? amountQuote = null,
            string? clientOrderId = null,
            bool? responseRequired = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel a single order. Either <paramref name="orderId"/> or <paramref name="clientOrderId"/> must be supplied.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/cancel-order/" /><br />
        /// Endpoint:<br />
        /// DELETE /v2/order
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market name</param>
        /// <param name="operatorId">["<c>operatorId</c>"] Trader/bot identifier (required)</param>
        /// <param name="orderId">["<c>orderId</c>"] Bitvavo order identifier (UUID)</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id (UUID); takes precedence if both ids are supplied</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitvavoCancelOrderResult>> CancelOrderAsync(
            string market,
            long operatorId,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a single order by id.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-order/" /><br />
        /// Endpoint:<br />
        /// GET /v2/order
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market name</param>
        /// <param name="orderId">["<c>orderId</c>"] Bitvavo order identifier</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id; takes precedence if both supplied</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitvavoOrder>> GetOrderAsync(string market, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get historical orders for a market.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-orders/" /><br />
        /// Endpoint:<br />
        /// GET /v2/orders
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market name (required)</param>
        /// <param name="limit">["<c>limit</c>"] Max results (1-1000, default 500)</param>
        /// <param name="startTime">["<c>start</c>"] Earliest timestamp</param>
        /// <param name="endTime">["<c>end</c>"] Latest timestamp</param>
        /// <param name="orderIdFrom">["<c>orderIdFrom</c>"] Pagination cursor (start)</param>
        /// <param name="orderIdTo">["<c>orderIdTo</c>"] Pagination cursor (end)</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitvavoOrder[]>> GetOrdersAsync(string market, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, string? orderIdFrom = null, string? orderIdTo = null, CancellationToken ct = default);

        /// <summary>
        /// Get currently open orders, optionally filtered by market or base asset.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-open-orders/" /><br />
        /// Endpoint:<br />
        /// GET /v2/ordersOpen
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Optional market filter</param>
        /// <param name="baseAsset">["<c>base</c>"] Optional base-asset filter</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitvavoOrder[]>> GetOpenOrdersAsync(string? market = null, string? baseAsset = null, CancellationToken ct = default);

        /// <summary>
        /// Get authenticated user trades for a market (max 24h window).
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-trade-history/" /><br />
        /// Endpoint:<br />
        /// GET /v2/trades
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Market name (required)</param>
        /// <param name="limit">["<c>limit</c>"] Max results (1-1000, default 500)</param>
        /// <param name="startTime">["<c>start</c>"] Earliest timestamp</param>
        /// <param name="endTime">["<c>end</c>"] Latest timestamp (max 24h from start)</param>
        /// <param name="tradeIdFrom">["<c>tradeIdFrom</c>"] Pagination cursor (start)</param>
        /// <param name="tradeIdTo">["<c>tradeIdTo</c>"] Pagination cursor (end)</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BitvavoUserTrade[]>> GetUserTradesAsync(string market, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, string? tradeIdFrom = null, string? tradeIdTo = null, CancellationToken ct = default);
    }
}
