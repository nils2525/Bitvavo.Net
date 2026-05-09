using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Page envelope returned by Bitvavo GET /v2/account/history.
    /// <see href="https://docs.bitvavo.com/docs/rest-api/get-transaction-history/" />.
    /// </summary>
    public record BitvavoTransactionHistory
    {
        /// <summary>
        /// ["<c>items</c>"] Transactions on this page (newest first).
        /// </summary>
        [JsonPropertyName("items")]
        public BitvavoTransaction[] Items { get; set; } = Array.Empty<BitvavoTransaction>();
        /// <summary>
        /// ["<c>currentPage</c>"] 1-based index of the current page.
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>totalPages</c>"] Total number of pages available for the requested filter.
        /// </summary>
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>maxItems</c>"] Maximum items per page (echo of the request parameter, 1..100).
        /// </summary>
        [JsonPropertyName("maxItems")]
        public int MaxItems { get; set; }
    }
}
