using System.Text.Json;
using Bitvavo.Net.Objects.Models.Socket;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;

namespace Bitvavo.Net.Clients.MessageHandlers
{
    /// <summary>
    /// Routes incoming websocket messages to the right query/subscription. Subscribe/unsubscribe
    /// responses are correlated by the echoed <c>requestId</c>; live data events are routed by
    /// <c>event</c> + per-message topic (e.g. <c>market</c> for trade updates).
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
            // Subscribe/unsubscribe acks carry the echoed `requestId`; this takes precedence over
            // the `event`/`action` evaluators below so each query response routes to its query.
            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("requestId"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("requestId")!,
            },
            // Most public stream messages (trade, ticker, ticker24h, candle, book) carry an "event" field.
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("event")!,
            },
            // Subscription/auth/error responses without a requestId fall back to the action.
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("action"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("action")!,
            }
        ];
    }
}
