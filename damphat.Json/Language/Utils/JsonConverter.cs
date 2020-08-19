using System.Globalization;

namespace damphat.Json.Language.Utils
{
    internal static class JsonConverter
    {
        /// <summary>
        ///     Convert a hex-digit to int 0..15
        ///     Return -1 if input is not a hex-digit
        /// </summary>
        public static int TryConvertHexChar2Int(char c)
        {
            if (c >= '0' && c <= '9') return c - '0';

            if (c >= 'a' && c <= 'f') return c - 'a' + 10;

            if (c >= 'A' && c <= 'F') return c - 'A' + 10;

            return -1;
        }

        // FIXME ToDouble("1e1000") got different result
        // - .NET Framework throws OverflowException
        // - .NET Core return Infinity
        public static double ToDouble(string str)
        {
            return double.Parse(str, CultureInfo.InvariantCulture);
        }
    }
}