namespace KIS.NET.Log.Output
{
    /// <summary>
    /// An interface declaring methods to log messages to an output target of some type. <br />
    /// Methods defined in this interface do this synchronously, thus named "Simple".
    /// </summary>
    /// <typeparam name="T">Type of the output target.</typeparam>
    public interface ISimpleLoggerOutput<in T> : ILoggerOutputBase
    {
        /// <summary>
        /// Interacts with the system using the given output target,
        /// while using the given log message a parameter.
        /// </summary>
        /// <param name="logMessage">Log's fully constructed message, including all wrapping formats.</param>
        /// <param name="outputTarget">Some output target for the log message,
        /// usually a stream or a file's path.</param>
        void OutputLog(string logMessage, T outputTarget);
    }
}