using System.IO;
using KIS.NET.IO.Utils;

namespace KIS.NET.IO.FileSystem
{
    /// <summary>
    /// A static utility class providing utility methods to manipulate files on the file system.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Checks weather the given path exists on the filesystem, assuming it's a file.
        /// </summary>
        /// <param name="filePath">File's path to check.</param>
        /// <returns>True if document exists, false otherwise.</returns>
        public static bool IsFileExisting(string filePath)
        {
            ValidationUtils.ValidatePathIsValid(filePath);
            return File.Exists(filePath);
        }

        /// <summary>
        /// Omits the file name from the given path so that only its' parent directory's full path remains.
        /// </summary>
        /// <param name="path">File path to get its' directory.</param>
        /// <returns>Parent directory's full path.</returns>
        public static string GetParentDirectoryPath(string path)
        {
            ValidationUtils.ValidatePathIsValid(path);
            return new FileInfo(path).DirectoryName;
        }

        /// <summary>
        /// Creates or overwrites a file on the file system at the given path.
        /// </summary>
        /// <param name="path">Path of the file to create or overwrite.</param>
        /// <returns>Reference to a file stream opened for that file.</returns>
        public static FileStream CreateFile(string path)
        {
            ValidationUtils.ValidatePathIsValid(path);
            return File.Create(path);
        }

        /// <summary>
        /// Deletes the file with the given file entirely from the file system.
        /// </summary>
        /// <param name="path">Path of the file to delete.</param>
        public static void DeleteFile(string path)
        {
            ValidationUtils.ValidatePathIsValid(path);
            File.Delete(path);
        }
    }
}