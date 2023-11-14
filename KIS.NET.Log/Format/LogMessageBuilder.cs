using System;
using KIS.NET.Log.Factory;

namespace KIS.NET.Log.Format
{
    /// <summary>
    /// A static class used to build log messages using a variable format.
    /// </summary>
    internal class LogMessageBuilder : ILogMessageBuilder
    {
        /// <summary>
        /// Initializes the static class by assigning a default log formatter.
        /// </summary>
        internal LogMessageBuilder()
        {
            StringLogFormat = new DateTimeWithSeparatorFormat<string>();
            ExceptionLogFormat =
                new ExceptionWithDateTimeAndSeparatorFormat(
                    new DiagnosticsFactory());
        }

        /// <inheritdoc />
        public string BuildMessage(string messageToLog, string additionalMessage = null)
        {
            return StringLogFormat.Format(messageToLog, additionalMessage);
        }

        /// <inheritdoc />
        public string BuildMessage(Exception exceptionToLog, string additionalMessage = null)
        {
            return ExceptionLogFormat.Format(exceptionToLog, additionalMessage);
        }

        /// <inheritdoc />
        public ILogFormat<string> StringLogFormat { get; set; }

        /// <inheritdoc />
        public ILogFormat<Exception> ExceptionLogFormat { get; set; }
    }
}