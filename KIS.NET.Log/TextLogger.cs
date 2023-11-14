using System;
using System.Threading.Tasks;
using KIS.NET.Log.Format;
using KIS.NET.Log.Output;

namespace KIS.NET.Log
{
    /// <summary>
    /// A class used to log some text to an arbitrary output target.
    /// </summary>
    /// <typeparam name="T">Type of the output target.</typeparam>
    public class TextLogger<T> :
        ILogger<string, T>
    {
        public TextLogger(ISimpleLoggerOutput<T> loggerOutput)
        {
            SimpleLoggerOutput = loggerOutput ??
                                 throw new ArgumentNullException(nameof(loggerOutput), "Logger output can't be null");
        }

        public TextLogger(IAsyncLoggerOutput<T> asyncLoggerOutput)
        {
            AsyncLoggerOutput = asyncLoggerOutput ??
                                throw new ArgumentNullException(nameof(asyncLoggerOutput),
                                    "Async logger output can't be null");
        }

        public TextLogger(ILoggerOutput<T> loggerOutput)
        {
            SimpleLoggerOutput = (ISimpleLoggerOutput<T>)loggerOutput ??
                                 throw new ArgumentNullException(nameof(loggerOutput), "Logger output can't be null");
            AsyncLoggerOutput = loggerOutput;
        }

        /// <summary>
        /// Logs the given object to the given output target, adding an optional additional message,
        /// and applying a custom format.
        /// </summary>
        /// <param name="exception">Object to log.</param>
        /// <param name="outputTarget">Log's output target, which could be
        /// a file, a stream, etc.</param>
        /// <param name="additionalMessage">An additional message to append
        /// to the log message.</param>
        /// <param name="logFormat">Reference to a formatter used to apply
        /// a custom format on the log message.
        /// If none is given, the default one is used.</param>
        public void Log(
            string exception,
            T outputTarget,
            string additionalMessage = null,
            ILogFormat<string> logFormat = null)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception), "Message to log can't be null");
            }

            if (string.IsNullOrWhiteSpace(exception))
            {
                throw new ArgumentException("Message to log can't be empty or contain only white spaces");
            }

            if (outputTarget == null)
            {
                throw new ArgumentNullException(nameof(outputTarget), "Output target can't be null");
            }

            string logMessage;
            if (logFormat != null)
            {
                logMessage = logFormat.Format(exception, additionalMessage);
            }
            else
            {
                if (MessageBuilder == null)
                {
                    MessageBuilder = new LogMessageBuilder();
                }

                logMessage = MessageBuilder.BuildMessage(exception, additionalMessage);
            }

            if (SimpleLoggerOutput == null)
            {
                throw new InvalidOperationException(
                    "Logger output can't be null - Please use only the constructor to manipulate this property");
            }

            SimpleLoggerOutput.OutputLog(logMessage, outputTarget);
        }

        public async Task LogAsync(string exception, T outputTarget, string additionalMessage = null,
            ILogFormat<string> logFormat = null)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception), "Message to log can't be null");
            }

            if (string.IsNullOrWhiteSpace(exception))
            {
                throw new ArgumentException("Message to log can't be empty or contain only white spaces");
            }

            if (outputTarget == null)
            {
                throw new ArgumentNullException(nameof(outputTarget), "Output target can't be null");
            }

            string logMessage;
            if (logFormat != null)
            {
                logMessage = logFormat.Format(exception, additionalMessage);
            }
            else
            {
                if (MessageBuilder == null)
                {
                    MessageBuilder = new LogMessageBuilder();
                }

                logMessage = MessageBuilder.BuildMessage(exception, additionalMessage);
            }

            if (AsyncLoggerOutput == null)
            {
                throw new InvalidOperationException(
                    "Logger output can't be null - Please use only the constructor to manipulate this property");
            }

            await AsyncLoggerOutput.OutputLogAsync(logMessage, outputTarget);
        }

        /// <summary>
        /// An object used to interact with the system to perform the actual log operation.
        /// </summary>
        public ISimpleLoggerOutput<T> SimpleLoggerOutput { get; set; }

        /// <inheritdoc />
        public IAsyncLoggerOutput<T> AsyncLoggerOutput { get; set; }

        /// <inheritdoc />
        public ILogMessageBuilder MessageBuilder { get; set; }
    }
}