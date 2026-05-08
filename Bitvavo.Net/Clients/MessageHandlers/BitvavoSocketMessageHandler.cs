using System.Text.Json;
using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;

namespace Bitvavo.Net.Clients.MessageHandlers
{
    /// <summary>
    /// Routes incoming websocket messages to the right subscription based on their <c>event</c> field
    /// (and the <c>market</c> field for per-market events).
    /// </summary>
    internal class BitvavoSocketMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitvavoExchange._serializerContext);

        public BitvavoSocketMessageHandler()
        {
            // Per-market events: routed by event-type and market.
            AddTopicMapping<BitvavoTradeUpdate>(x => x.Market);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [
            // Most public stream messages (trade, ticker, ticker24h, candle, book) carry an "event" field.
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("event")!,
            },
            // Subscription/auth/error responses carry an "action" field.
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("action"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("action")!,
            }
        ];
    }
}
