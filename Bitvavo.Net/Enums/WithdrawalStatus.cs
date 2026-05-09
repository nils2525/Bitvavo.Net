using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Enums
{
    /// <summary>
    /// Withdrawal status as returned by Bitvavo /withdrawalHistory.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalStatus>))]
    public enum WithdrawalStatus
    {
        /// <summary>
        /// ["<c>awaiting_processing</c>"] Awaiting processing
        /// </summary>
        [Map("awaiting_processing")]
        AwaitingProcessing,
        /// <summary>
        /// ["<c>awaiting_email_confirmation</c>"] Awaiting email confirmation
        /// </summary>
        [Map("awaiting_email_confirmation")]
        AwaitingEmailConfirmation,
        /// <summary>
        /// ["<c>awaiting_bitvavo_inspection</c>"] Awaiting Bitvavo inspection
        /// </summary>
        [Map("awaiting_bitvavo_inspection")]
        AwaitingBitvavoInspection,
        /// <summary>
        /// ["<c>approved</c>"] Approved
        /// </summary>
        [Map("approved")]
        Approved,
        /// <summary>
        /// ["<c>sending</c>"] Sending
        /// </summary>
        [Map("sending")]
        Sending,
        /// <summary>
        /// ["<c>in_mempool</c>"] In mempool
        /// </summary>
        [Map("in_mempool")]
        InMempool,
        /// <summary>
        /// ["<c>processed</c>"] Processed
        /// </summary>
        [Map("processed")]
        Processed,
        /// <summary>
        /// ["<c>completed</c>"] Completed
        /// </summary>
        [Map("completed")]
        Completed,
        /// <summary>
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [Map("canceled")]
        Canceled
    }
}
