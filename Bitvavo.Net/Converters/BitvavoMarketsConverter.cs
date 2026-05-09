using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bitvavo.Net.Converters
{
    /// <summary>
    /// Custom converter for the <c>markets</c> property of a Bitvavo subscribe/unsubscribe channel.
    /// Bitvavo accepts two distinct wire shapes for this field on the same channel:
    /// <list type="bullet">
    ///     <item><description>An array of market names: <c>"markets": ["BTC-EUR", "ETH-EUR"]</c>.</description></item>
    ///     <item><description>The bare wildcard string: <c>"markets": "*"</c>, which subscribes to every currently listed market.</description></item>
    /// </list>
    /// Docs: <a href="https://docs.bitvavo.com/docs/websocket-api/track-your-orders/" />
    /// <para>
    /// On write, a sentinel value of a single-element <c>string[] { "*" }</c> is serialized as the bare
    /// string <c>"*"</c>; any other content (including an empty array) is serialized as a normal JSON
    /// array. On read, both shapes are accepted - a bare string is materialized as a single-element
    /// array - so round-tripping is symmetric.
    /// </para>
    /// </summary>
    internal class BitvavoMarketsConverter : JsonConverter<string[]>
    {
        public override string[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                return value == null ? Array.Empty<string>() : new[] { value };
            }

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var list = new List<string>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        return list.ToArray();
                    if (reader.TokenType == JsonTokenType.String)
                    {
                        var s = reader.GetString();
                        if (s != null)
                            list.Add(s);
                    }
                }
                return list.ToArray();
            }

            return Array.Empty<string>();
        }

        public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
        {
            if (value != null && value.Length == 1 && value[0] == "*")
            {
                writer.WriteStringValue("*");
                return;
            }

            writer.WriteStartArray();
            if (value != null)
            {
                foreach (var market in value)
                    writer.WriteStringValue(market);
            }
            writer.WriteEndArray();
        }
    }
}
