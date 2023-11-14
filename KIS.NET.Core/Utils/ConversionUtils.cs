using System;

namespace KIS.NET.Core.Utils
{
    /// <summary>
    /// A static utility class providing utility methods to convert between different types of objects.
    /// </summary>
    public static class ConversionUtils
    {
        /// <summary>
        /// Converts a given string to the given explicit type, which is known at compile time,
        /// assuming it can be converted to easilly.
        /// </summary>
        /// <typeparam name="T">Type of the object we're converting to.</typeparam>
        /// <param name="convertedString">The string to convert.</param>
        /// <returns>Reference to the converted type.</returns>
        public static T ConvertFromString<T>(string convertedString)
        {
            return typeof(T).IsEnum
                ? (T)Enum.Parse(typeof(T), convertedString)
                : (T)Convert.ChangeType((object)convertedString, typeof(T));
        }

        /// <summary>
        /// Converts a given string to the given explicit type, which is known only at runtime,
        /// assuming it can be converted to easilly.
        /// Returns an object.
        /// </summary>
        /// <param name="convertedString">The string to convert.</param>
        /// <param name="typeToConvertTo">The type we're converting to.</param>
        /// <returns>Reference to the converted object.</returns>
        public static object ConvertFromString(string convertedString, Type typeToConvertTo)
        {
            return typeToConvertTo.IsEnum
                ? Enum.Parse(typeToConvertTo, convertedString)
                : Convert.ChangeType((object)convertedString, typeToConvertTo);
        }
    }
}