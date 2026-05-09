using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Enums
{
    /// <summary>
    /// Transaction type as returned by Bitvavo GET /v2/account/history.
    /// <see href="https://docs.bitvavo.com/docs/rest-api/get-transaction-history/" />.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BitvavoTransactionType>))]
    public enum BitvavoTransactionType
    {
        /// <summary>
        /// ["<c>sell</c>"] Sell trade (sent base, received quote).
        /// </summary>
        [Map("sell")]
        Sell,
        /// <summary>
        /// ["<c>buy</c>"] Buy trade (sent quote, received base).
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// ["<c>staking</c>"] Staking reward / payout.
        /// </summary>
        [Map("staking")]
        Staking,
        /// <summary>
        /// ["<c>fixed_staking</c>"] Fixed-term staking reward / payout.
        /// </summary>
        [Map("fixed_staking")]
        FixedStaking,
        /// <summary>
        /// ["<c>deposit</c>"] Deposit (fiat or digital).
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// ["<c>withdrawal</c>"] Withdrawal (fiat or digital).
        /// </summary>
        [Map("withdrawal")]
        Withdrawal,
        /// <summary>
        /// ["<c>affiliate</c>"] Affiliate program payout.
        /// </summary>
        [Map("affiliate")]
        Affiliate,
        /// <summary>
        /// ["<c>distribution</c>"] Asset distribution (e.g. airdrop, hard fork).
        /// </summary>
        [Map("distribution")]
        Distribution,
        /// <summary>
        /// ["<c>internal_transfer</c>"] Internal transfer between Bitvavo accounts.
        /// </summary>
        [Map("internal_transfer")]
        InternalTransfer,
        /// <summary>
        /// ["<c>withdrawal_cancelled</c>"] Withdrawal cancelled (refund of a previously initiated withdrawal).
        /// </summary>
        [Map("withdrawal_cancelled")]
        WithdrawalCancelled,
        /// <summary>
        /// ["<c>rebate</c>"] Fee rebate.
        /// </summary>
        [Map("rebate")]
        Rebate,
        /// <summary>
        /// ["<c>loan</c>"] Loan-related transaction.
        /// </summary>
        [Map("loan")]
        Loan,
        /// <summary>
        /// ["<c>external_transferred_funds</c>"] Funds transferred from an external source.
        /// </summary>
        [Map("external_transferred_funds")]
        ExternalTransferredFunds,
        /// <summary>
        /// ["<c>manually_assigned</c>"] Manually assigned by Bitvavo (support adjustment).
        /// </summary>
        [Map("manually_assigned")]
        ManuallyAssigned
    }
}
