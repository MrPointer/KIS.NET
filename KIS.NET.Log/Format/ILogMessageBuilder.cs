using System;

namespace KIS.NET.Log.Format
{
    /// <summary>
    /// An interface declaring methods to wrap the <see cref="T:KIS.NET.Log.Format.ILogFormat`1" /> interface,
    /// providing wrapper methods for all concrete types implementing it.
    /// </summary>
    public interface ILogMessageBuilder
    {
        /// <summary>Builds a log message based on the given parameters.</summary>
        /// <param name="messageToLog">Log message's inner message.</param>
        /// <param name="additionalMessage">Additional message to append to the log object.</param>
        /// <returns>Formatted log message.</returns>
        string BuildMessage(string messageToLog, string additionalMessage = null);

        /// <summary>Builds a log message based on the given parameters.</summary>
        /// <param name="exceptionToLog">Exception to build the log message on.</param>
        /// <param name="additionalMessage">Additional message to append to the log object.</param>
        /// <returns>Formatted log message.</returns>
        string BuildMessage(Exception exceptionToLog, string additionalMessage = null);

        /// <summary>
        /// Gets or sets the format to use on logged <see cref="T:System.String" /> objects.
        /// </summary>
        ILogFormat<string> StringLogFormat { get; set; }

        /// <summary>
        /// Gets or sets the format to use on logged <see cref="T:System.Exception" /> objects.
        /// </summary>
        ILogFormat<Exception> ExceptionLogFormat { get; set; }
    }
}