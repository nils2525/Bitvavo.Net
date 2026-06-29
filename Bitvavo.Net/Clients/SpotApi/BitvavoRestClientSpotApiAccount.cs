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
        public Task<HttpResult<BitvavoAccount>> GetAccountAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/account", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoAccount>(request, null, ct);
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public Task<HttpResult<BitvavoBalance[]>> GetBalancesAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/balance", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoBalance[]>(request, parameters, ct);
        }

        #endregion

        #region Get Deposit History

        /// <inheritdoc />
        public Task<HttpResult<BitvavoDeposit[]>> GetDepositHistoryAsync(string? symbol = null, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit);
            parameters.Add("start", startTime);
            parameters.Add("end", endTime);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/depositHistory", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoDeposit[]>(request, parameters, ct);
        }

        #endregion

        #region Get Withdrawal History

        /// <inheritdoc />
        public Task<HttpResult<BitvavoWithdrawal[]>> GetWithdrawalHistoryAsync(string? symbol = null, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit);
            parameters.Add("start", startTime);
            parameters.Add("end", endTime);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/withdrawalHistory", BitvavoExchange.RateLimiter.Rest, 5, true);
            return _baseClient.SendAsync<BitvavoWithdrawal[]>(request, parameters, ct);
        }

        #endregion

        #region Get Transaction History

        /// <inheritdoc />
        public Task<HttpResult<BitvavoTransactionHistory>> GetTransactionHistoryAsync(DateTime? fromDate = null, DateTime? toDate = null, int? page = null, int? maxItems = null, BitvavoTransactionType? type = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("fromDate", fromDate);
            parameters.Add("toDate", toDate);
            parameters.Add("page", page);
            parameters.Add("maxItems", maxItems);
            parameters.Add("type", type);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/account/history", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoTransactionHistory>(request, parameters, ct);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public Task<HttpResult<BitvavoWithdrawalResult>> WithdrawAsync(string symbol, decimal amount, string address, string? paymentId = null, bool? addWithdrawalFee = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitvavoExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("amount", amount);
            parameters.Add("address", address);
            parameters.Add("paymentId", paymentId);
            parameters.Add("addWithdrawalFee", addWithdrawalFee);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v2/withdrawal", BitvavoExchange.RateLimiter.Rest, 1, true);
            return _baseClient.SendAsync<BitvavoWithdrawalResult>(request, parameters, ct);
        }

        #endregion
    }
}
