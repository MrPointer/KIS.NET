using System.IO;
using KIS.NET.IO.Utils;

namespace KIS.NET.IO.FileSystem
{
    /// <summary>
    /// A static utility class providing utility method to manipulate directories on the file system.
    /// </summary>
    public static class DirectoryUtils
    {
        /// <summary>
        /// Checks weather the given path exists on the filesystem, assuming it's a file.
        /// </summary>
        /// <param name="directoryPath">Directory's path to check.</param>
        /// <returns>True if document exists, false otherwise.</returns>
        public static bool IsDirectoryExisting(string directoryPath)
        {
            ValidationUtils.ValidatePathIsValid(directoryPath);
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// Creates a directory (including sub-directories) at the specified path.
        /// </summary>
        /// <param name="directoryPath">Path to create the directory at.</param>
        /// <returns>Reference to a directory info object wrapping the created directory.</returns>
        public static DirectoryInfo CreateDirectory(string directoryPath)
        {
            ValidationUtils.ValidatePathIsValid(directoryPath);
            return Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        /// Deletes a directory at the specified path, and optionally all of its' sub-directories and files.
        /// </summary>
        /// <param name="directoryPath">Path of the upper directory to delete.</param>
        /// <param name="isRecursive">Value indicating weather sub-directories and files should be deleted as well.
        /// Set to false by default.</param>
        public static void DeleteDirectory(string directoryPath, bool isRecursive = false)
        {
            ValidationUtils.ValidatePathIsValid(directoryPath);
            Directory.Delete(directoryPath, isRecursive);
        }
    }
}