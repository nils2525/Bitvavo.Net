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
            []
        );
    }
}
