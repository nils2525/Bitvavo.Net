using CryptoExchange.Net.Objects.Errors;

namespace Bitvavo.Net
{
    /// <summary>
    /// Bitvavo error code mappings.
    /// <see href="https://docs.bitvavo.com/" />
    /// </summary>
    internal static class BitvavoErrors
    {
        internal static ErrorMapping RestErrorMapping { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API key", "105", "108", "109", "300"),
                new ErrorInfo(ErrorType.RateLimitRequest, false, "Rate limit exceeded", "110", "112"),
                new ErrorInfo(ErrorType.UnknownSymbol, false, "Unknown market", "203", "205"),
            ]
        );
    }
}
