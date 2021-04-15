namespace Core.Extensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        ///     String To Boolen
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool ToBool(this string source)
        {
            source ??= "false";
            source = source == "1" ? "true" : source;
            bool.TryParse(source.Trim(), out var result);
            return result;
        }
    }
}