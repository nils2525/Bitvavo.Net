using System.Net.Http.Headers;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;

namespace Bitvavo.Net.Clients.MessageHandlers
{
    internal class BitvavoRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;
        public override bool RequiresSeekableStream => true;
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitvavoExchange._serializerContext);

        public BitvavoRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        private Error ParseErrorInternal(JsonElement rootElement)
        {
            var code = rootElement.TryGetProperty("errorCode", out var codeProp) ? codeProp.ToString() : "?";

            string? reason = null;
            if (rootElement.TryGetProperty("error", out var errorProp))
                reason = errorProp.GetString();
            reason ??= rootElement.TryGetProperty("message", out var messageProp) ? messageProp.GetString() : null;

            return new ServerError(code, _errorMapping.GetErrorInfo(code, reason));
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            return ParseErrorInternal(document!.RootElement);
        }

        public override async ValueTask<Error?> CheckForErrorResponse(RequestDefinition request, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            if (document!.RootElement.ValueKind is JsonValueKind.Array)
                return null;

            // Bitvavo error responses always carry an "errorCode" property; success responses for non-array endpoints don't.
            if (document.RootElement.TryGetProperty("errorCode", out _))
                return ParseErrorInternal(document.RootElement);

            return await base.CheckForErrorResponse(request, responseHeaders, responseStream).ConfigureAwait(false);
        }
    }
}
