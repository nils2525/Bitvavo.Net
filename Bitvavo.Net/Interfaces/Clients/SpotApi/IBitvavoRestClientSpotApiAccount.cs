using Bitvavo.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Bitvavo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Bitvavo Spot account endpoints (balances, fees, deposits/withdrawals).
    /// </summary>
    public interface IBitvavoRestClientSpotApiAccount
    {
        /// <summary>
        /// Get the current account fee tier and rolling 30-day EUR volume.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-account-fees/" /><br />
        /// Endpoint:<br />
        /// GET /v2/account
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoAccount>> GetAccountAsync(CancellationToken ct = default);

        /// <summary>
        /// Get balances for one asset or all assets with a non-zero balance.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-account-balance/" /><br />
        /// Endpoint:<br />
        /// GET /v2/balance
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Optional asset filter (for example <c>BTC</c>)</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoBalance[]>> GetBalancesAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get past deposits for the account.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-deposit-history/" /><br />
        /// Endpoint:<br />
        /// GET /v2/depositHistory
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Optional asset filter</param>
        /// <param name="limit">["<c>limit</c>"] Max results (1-1000, default 500)</param>
        /// <param name="startTime">["<c>start</c>"] Earliest timestamp</param>
        /// <param name="endTime">["<c>end</c>"] Latest timestamp</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoDeposit[]>> GetDepositHistoryAsync(string? symbol = null, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get past withdrawals for the account.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/get-withdrawal-history/" /><br />
        /// Endpoint:<br />
        /// GET /v2/withdrawalHistory
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Optional asset filter</param>
        /// <param name="limit">["<c>limit</c>"] Max results (1-1000, default 500)</param>
        /// <param name="startTime">["<c>start</c>"] Earliest timestamp</param>
        /// <param name="endTime">["<c>end</c>"] Latest timestamp</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoWithdrawal[]>> GetWithdrawalHistoryAsync(string? symbol = null, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Submit a withdrawal request.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.bitvavo.com/docs/rest-api/withdraw-assets/" /><br />
        /// Endpoint:<br />
        /// POST /v2/withdrawal
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Asset to withdraw (for example <c>BTC</c>)</param>
        /// <param name="amount">["<c>amount</c>"] Amount to withdraw</param>
        /// <param name="address">["<c>address</c>"] Destination address</param>
        /// <param name="paymentId">["<c>paymentId</c>"] Optional payment id / memo</param>
        /// <param name="addWithdrawalFee">["<c>addWithdrawalFee</c>"] Add the fee on top of <paramref name="amount"/> instead of subtracting it</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitvavoWithdrawalResult>> WithdrawAsync(string symbol, decimal amount, string address, string? paymentId = null, bool? addWithdrawalFee = null, CancellationToken ct = default);
    }
}
