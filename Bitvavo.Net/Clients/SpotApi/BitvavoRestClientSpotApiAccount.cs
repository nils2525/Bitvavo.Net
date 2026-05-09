using Bitvavo.Net.Enums;
using Bitvavo.Net.Interfaces.Clients.SpotApi;
using Bitvavo.Net.Objects.Models;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;

namespace Bitvavo.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitvavoRestClientSpotApiAccount : IBitvavoRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitvavoRestClientSpotApi _baseClient;

        internal BitvavoRestClientSpotApiAccount(BitvavoRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Account

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoAccount>> GetAccountAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/account", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoAccount>(request, null, ct);
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoBalance[]>> GetBalancesAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/balance", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoBalance[]>(request, parameters, ct);
        }

        #endregion

        #region Get Deposit History

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoDeposit[]>> GetDepositHistoryAsync(string? symbol = null, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalMilliseconds("start", startTime);
            parameters.AddOptionalMilliseconds("end", endTime);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/depositHistory", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoDeposit[]>(request, parameters, ct);
        }

        #endregion

        #region Get Withdrawal History

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoWithdrawal[]>> GetWithdrawalHistoryAsync(string? symbol = null, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalMilliseconds("start", startTime);
            parameters.AddOptionalMilliseconds("end", endTime);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/withdrawalHistory", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoWithdrawal[]>(request, parameters, ct);
        }

        #endregion

        #region Get Transaction History

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoTransactionHistory>> GetTransactionHistoryAsync(DateTime? fromDate = null, DateTime? toDate = null, int? page = null, int? maxItems = null, BitvavoTransactionType? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalMilliseconds("fromDate", fromDate);
            parameters.AddOptionalMilliseconds("toDate", toDate);
            parameters.AddOptional("page", page);
            parameters.AddOptional("maxItems", maxItems);
            parameters.AddOptionalEnum("type", type);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/account/history", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoTransactionHistory>(request, parameters, ct);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public Task<WebCallResult<BitvavoWithdrawalResult>> WithdrawAsync(string symbol, decimal amount, string address, string? paymentId = null, bool? addWithdrawalFee = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddString("amount", amount);
            parameters.Add("address", address);
            parameters.AddOptional("paymentId", paymentId);
            parameters.AddOptional("addWithdrawalFee", addWithdrawalFee);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/withdrawal", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoWithdrawalResult>(request, parameters, ct);
        }

        #endregion
    }
}
