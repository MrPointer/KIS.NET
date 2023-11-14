using System;

namespace KIS.NET.IO.Utils
{
    /// <summary>
    /// A static utility class used to provide internal validation utility methods.
    /// </summary>
    internal static class ValidationUtils
    {
        /// <summary>
        /// Validates the given string can represent a valid path on the file system.
        /// </summary>
        /// <param name="pathParameter">A string representing a file path.</param>
        internal static void ValidatePathIsValid(string pathParameter)
        {
            if (pathParameter == null)
                throw new ArgumentNullException(nameof(pathParameter), "Given file path can't be null");
            if (string.IsNullOrWhiteSpace(pathParameter))
                throw new ArgumentException("Given file path can't be empty or contain only whitespaces",
                    pathParameter);
        }
    }
}