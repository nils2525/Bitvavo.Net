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

    [JsonSerializable(typeof(BitvavoBalance))]
    [JsonSerializable(typeof(BitvavoBalance[]))]
    [JsonSerializable(typeof(BitvavoAccount))]
    [JsonSerializable(typeof(BitvavoAccountFees))]
    [JsonSerializable(typeof(BitvavoOrder))]
    [JsonSerializable(typeof(BitvavoOrder[]))]
    [JsonSerializable(typeof(BitvavoOrderFill))]
    [JsonSerializable(typeof(BitvavoOrderFill[]))]
    [JsonSerializable(typeof(BitvavoCancelOrderResult))]
    [JsonSerializable(typeof(BitvavoUserTrade))]
    [JsonSerializable(typeof(BitvavoUserTrade[]))]
    [JsonSerializable(typeof(BitvavoDeposit))]
    [JsonSerializable(typeof(BitvavoDeposit[]))]
    [JsonSerializable(typeof(BitvavoWithdrawal))]
    [JsonSerializable(typeof(BitvavoWithdrawal[]))]
    [JsonSerializable(typeof(BitvavoWithdrawalResult))]
    [JsonSerializable(typeof(BitvavoOrderBook))]
    [JsonSerializable(typeof(BitvavoOrderBookEntry))]
    [JsonSerializable(typeof(BitvavoOrderBookEntry[]))]

    [JsonSerializable(typeof(BitvavoTradeUpdate))]
    [JsonSerializable(typeof(BitvavoTicker24hUpdate))]
    [JsonSerializable(typeof(BitvavoOrderUpdate))]
    [JsonSerializable(typeof(BitvavoFillUpdate))]
    [JsonSerializable(typeof(BitvavoOrderBookUpdate))]
    [JsonSerializable(typeof(BitvavoSocketRequest))]
    [JsonSerializable(typeof(BitvavoSubscriptionChannel))]
    [JsonSerializable(typeof(BitvavoSubscriptionResponse))]
    [JsonSerializable(typeof(BitvavoAuthRequest))]
    [JsonSerializable(typeof(BitvavoAuthResponse))]

    [JsonSerializable(typeof(IDictionary<string, object>))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(string[]))]
    [JsonSerializable(typeof(bool))]
    internal partial class BitvavoSourceGenerationContext : JsonSerializerContext
    { }
}
