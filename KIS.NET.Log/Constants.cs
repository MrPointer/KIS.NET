using System.Text;

namespace KIS.NET.Log
{
    /// <summary>
    /// A static class listing various constant values used by the library.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets the default encoding used by <see cref="T:KIS.NET.Log.Output.ISimpleLoggerOutput`1" /> implementations.
        /// </summary>
        internal static readonly Encoding DefaultEncoding;

        /// <summary>
        /// Gets the default file extension used by <see cref="T:KIS.NET.Log.Output.ISimpleLoggerOutput`1" />
        /// implementations that write to files on the file system.
        /// </summary>
        internal const string DEFAULT_FILE_EXTENSION = ".log";

        static Constants()
        {
            DefaultEncoding = Encoding.Unicode;
        }
    }
}