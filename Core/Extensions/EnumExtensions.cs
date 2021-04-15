using System;

namespace Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     String To Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string source)
        {
            return (T) Enum.Parse(typeof(T), source, true);
        }
    }
}