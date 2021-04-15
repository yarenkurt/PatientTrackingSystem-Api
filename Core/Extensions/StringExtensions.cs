using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Substring(0, length)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string source, int length)
        {
            if (string.IsNullOrEmpty(source)) return "";
            return source.Length > length ? source.Substring(0, length) : source;
        }

        /// <summary>
        ///     Substring(source.Length - length, length)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string source, int length)
        {
            if (string.IsNullOrEmpty(source)) return "";

            return source.Length > length ? source.Substring(source.Length - length, length) : source;
        }

        /// <summary>
        ///     String To Base64
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBase64String(this string value)
        {
            return string.IsNullOrEmpty(value) ? "" : Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        ///     Base64 To String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FromBase64String(this string value)
        {
            return string.IsNullOrEmpty(value) ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }

        /// <summary>
        ///     Reverse
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Reverse(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return string.Empty;
            var chars = source.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        ///     ToPhone Length(10) *clears if the first number is zero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPhone(this string value)
        {
            value ??= "";
            value = value.Replace(" ", "");
            if (value.Length >= 10 && value.Length <= 11) return value.Length == 10 ? value : value.Substring(1, 10);

            return "";
        }

        /// <summary>
        ///     Converts into a list based on the type of separator within a text
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<string> Split(this string source, string separator)
        {
            if (string.IsNullOrEmpty(source)) return new List<string>();
            var result = new List<string>();
            if (source.IndexOf(separator, StringComparison.Ordinal) < 0)
            {
                result.Add(source);
                return result;
            }

            var sources = source.Split(new[] {separator}, StringSplitOptions.None);
            result.AddRange(sources.Where(item => !string.IsNullOrEmpty(item)));
            return result;
        }

        /// <summary>
        ///     HTML tags clear
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string StripHtmlTags(this string source)
        {
            return Regex.Replace(source, "<.*?>|&.*?;", string.Empty);
        }

        /// <summary>
        ///     Clears all characters except letters and numbers
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ClearSymbol(this string source)
        {
            var values = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V', 'Y', 'Z', 'W', 'X',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u', 'v', 'y', 'z', 'w', 'x',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
            var builder = new StringBuilder();
            foreach (var ch in source.Where(ch => values.Contains(ch))) builder.Append(ch);

            return builder.ToString();
        }


        /// <summary>
        ///     Pluralize s, ies, ves ...
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Pluralize(this string source)
        {
            if (string.IsNullOrEmpty(source)) return "";

            return source.EndsWith("y", StringComparison.OrdinalIgnoreCase) &&
                   !source.EndsWith("ay", StringComparison.OrdinalIgnoreCase) &&
                   !source.EndsWith("ey", StringComparison.OrdinalIgnoreCase) &&
                   !source.EndsWith("iy", StringComparison.OrdinalIgnoreCase) &&
                   !source.EndsWith("oy", StringComparison.OrdinalIgnoreCase) &&
                   !source.EndsWith("uy", StringComparison.OrdinalIgnoreCase)
                ? $"{source[..^1]}ies"
                : source.EndsWith("us", StringComparison.InvariantCultureIgnoreCase) || source.EndsWith("ss", StringComparison.InvariantCultureIgnoreCase) ||
                  source.EndsWith("x", StringComparison.InvariantCultureIgnoreCase) || source.EndsWith("ch", StringComparison.InvariantCultureIgnoreCase) ||
                  source.EndsWith("sh", StringComparison.InvariantCultureIgnoreCase)
                    ? $"{source}es"
                    : source.EndsWith("f", StringComparison.InvariantCultureIgnoreCase) && source.Length > 1
                        ? $"{source[..^1]}ves"
                        : source.EndsWith("fe", StringComparison.InvariantCultureIgnoreCase) && source.Length > 2
                            ? $"{source[..^2]}ves"
                            : source.EndsWith("s", StringComparison.InvariantCultureIgnoreCase)
                                ? source
                                : $"{source}s";
        }
    }
}