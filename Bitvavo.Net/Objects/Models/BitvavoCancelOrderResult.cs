using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Response payload of Bitvavo DELETE /v2/order.
    /// </summary>
    public record BitvavoCancelOrderResult
    {
        /// <summary>
        /// ["<c>orderId</c>"] Bitvavo order identifier (UUID) of the canceled order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order identifier (UUID), only present when supplied on cancel
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>operatorId</c>"] Operator identifier echoed from the cancel request
        /// </summary>
        [JsonPropertyName("operatorId")]
        public long? OperatorId { get; set; }
    }
}
