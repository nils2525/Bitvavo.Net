using Bitvavo.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Objects.Models
{
    /// <summary>
    /// Asset info as returned by Bitvavo GET /v2/assets
    /// </summary>
    public record BitvavoAsset
    {
        /// <summary>
        /// ["<c>symbol</c>"] Asset short name (for example <c>BTC</c>)
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Display name (for example <c>Bitcoin</c>)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>decimals</c>"] Number of decimals supported for this asset
        /// </summary>
        [JsonPropertyName("decimals")]
        public int Decimals { get; set; }
        /// <summary>
        /// ["<c>depositFee</c>"] Deposit fee
        /// </summary>
        [JsonPropertyName("depositFee")]
        public decimal DepositFee { get; set; }
        /// <summary>
        /// ["<c>depositConfirmations</c>"] Confirmations required for a deposit to be credited
        /// </summary>
        [JsonPropertyName("depositConfirmations")]
        public int DepositConfirmations { get; set; }
        /// <summary>
        /// ["<c>depositStatus</c>"] Current deposit status of the asset
        /// </summary>
        [JsonPropertyName("depositStatus")]
        public AssetStatus DepositStatus { get; set; }
        /// <summary>
        /// ["<c>withdrawalFee</c>"] Withdrawal fee
        /// </summary>
        [JsonPropertyName("withdrawalFee")]
        public decimal WithdrawalFee { get; set; }
        /// <summary>
        /// ["<c>withdrawalMinAmount</c>"] Minimum withdrawal amount
        /// </summary>
        [JsonPropertyName("withdrawalMinAmount")]
        public decimal WithdrawalMinAmount { get; set; }
        /// <summary>
        /// ["<c>withdrawalStatus</c>"] Current withdrawal status of the asset
        /// </summary>
        [JsonPropertyName("withdrawalStatus")]
        public AssetStatus WithdrawalStatus { get; set; }
        /// <summary>
        /// ["<c>networks</c>"] List of networks the asset is supported on
        /// </summary>
        [JsonPropertyName("networks")]
        public string[] Networks { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>message</c>"] Optional message regarding the current status of the asset
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
