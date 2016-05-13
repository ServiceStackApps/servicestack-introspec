namespace ServiceStack.Documentation.Extensions
{
    using System;

    /// <summary>
    /// Collection of extension methods used for calling Func to get value if value is empty/default
    /// </summary>
    public static class GetValueExtensions
    {
        public static string GetIfNullOrEmpty(this string value, Func<string> getValue)
        {
            return value.IsNullOrEmpty() ? getValue() : value;
        }

        public static T GetIfNull<T>(this T value, Func<T> getValue)
            where T : class
        {
            return value ?? getValue();
        }

        public static T[] GetIfNullOrEmpty<T>(this T[] array, Func<T[]> getValue)
        {
            return array.IsNullOrEmpty() ? getValue() : array;
        }

        public static T? GetIfNoValue<T>(this T? value, Func<T?> getValue)
            where T : struct
        {
            return !value.HasValue ? getValue() : value;
        }
    }
}