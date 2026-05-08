using Bitvavo.Net.Objects.Models;
using Bitvavo.Net.Objects.Models.Socket;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Converters
{
    [JsonSerializable(typeof(BitvavoServerTime))]
    [JsonSerializable(typeof(BitvavoMarket))]
    [JsonSerializable(typeof(BitvavoMarket[]))]
    [JsonSerializable(typeof(BitvavoAsset))]
    [JsonSerializable(typeof(BitvavoAsset[]))]
    [JsonSerializable(typeof(BitvavoTicker24h))]
    [JsonSerializable(typeof(BitvavoTicker24h[]))]
    [JsonSerializable(typeof(BitvavoTickerPrice))]
    [JsonSerializable(typeof(BitvavoTickerPrice[]))]
    [JsonSerializable(typeof(BitvavoTickerBook))]
    [JsonSerializable(typeof(BitvavoTickerBook[]))]
    [JsonSerializable(typeof(BitvavoPublicTrade))]
    [JsonSerializable(typeof(BitvavoPublicTrade[]))]

    [JsonSerializable(typeof(BitvavoTradeUpdate))]
    [JsonSerializable(typeof(BitvavoTicker24hUpdate))]
    [JsonSerializable(typeof(BitvavoSocketRequest))]
    [JsonSerializable(typeof(BitvavoSubscriptionChannel))]
    [JsonSerializable(typeof(BitvavoSubscriptionResponse))]

    [JsonSerializable(typeof(IDictionary<string, object>))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(string[]))]
    [JsonSerializable(typeof(bool))]
    internal partial class BitvavoSourceGenerationContext : JsonSerializerContext
    { }
}
