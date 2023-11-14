using System.Threading.Tasks;

namespace KIS.NET.Log.Output
{
    /// <summary>
    /// An interface declaring methods to interact with 3rd party components,
    /// such as the file system or a DB. <br />
    /// Interaction is done asynchronously.
    /// </summary>
    /// <typeparam name="T">Type of the output target.</typeparam>
    public interface IAsyncLoggerOutput<in T> : ILoggerOutputBase
    {
        /// <summary>
        /// Interacts with the system using the given output target,
        /// while using the given log message a parameter. <br />
        /// This operation is done asynchronously.
        /// </summary>
        /// <param name="logMessage">Log's fully constructed message, including all wrapping formats.</param>
        /// <param name="outputTarget">Some output target for the log message,
        /// usually a stream or a file's path.</param>
        Task OutputLogAsync(string logMessage, T outputTarget);
    }
}