namespace Core.Extensions
{
    public static class NumericExtensions
    {
        /// <summary>
        ///     String To Boolean
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string source)
        {
            return double.TryParse(source ?? "", out _);
        }

        /// <summary>
        ///     String To Byte
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte ToByte(this string source)
        {
            source ??= "0";
            byte.TryParse(source.Trim(), out var result);
            return result;
        }

        /// <summary>
        ///     String To Short
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static short ToShort(this string source)
        {
            source ??= "0";
            short.TryParse(source.Trim(), out var result);
            return result;
        }

        /// <summary>
        ///     String To Integer
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToInt(this string source)
        {
            source ??= "0";
            int.TryParse(source.Trim(), out var result);
            return result;
        }

        /// <summary>
        ///     String To Double
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static double ToDouble(this string source)
        {
            source ??= "0";
            source = source.Replace(".", "").Replace(",", ".");
            double.TryParse(source.Trim(), out var result);
            return result;
        }

        /// <summary>
        ///     String To Decimal
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string source)
        {
            source ??= "0";
            source = source.Replace(".", "").Replace(",", ".");
            decimal.TryParse(source.Trim(), out var result);
            return result;
        }
    }
}