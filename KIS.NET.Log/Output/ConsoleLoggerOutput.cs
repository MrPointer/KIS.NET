using System;
using System.Text;
using KIS.NET.Core;

namespace KIS.NET.Log.Output
{
    /// <summary>
    /// A class used to output log messages to the <see cref="T:System.Console" />.
    /// </summary>
    public class ConsoleLoggerOutput : ISimpleLoggerOutput<NullObject>
    {
        /// <inheritdoc />
        public void OutputLog(string logMessage, NullObject outputTarget = null)
        {
            if (logMessage == null)
            {
                throw new ArgumentNullException(nameof(logMessage), "Message to log can't be null");
            }

            if (string.IsNullOrWhiteSpace(logMessage))
            {
                throw new ArgumentException("Message to log can't be empty or contain only white spaces",
                    nameof(logMessage));
            }

            Console.WriteLine(logMessage);
        }

        /// <inheritdoc />
        public Encoding EncodingToUse { get; set; }
    }
}