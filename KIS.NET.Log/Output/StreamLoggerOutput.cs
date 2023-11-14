using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KIS.NET.Log.Output
{
    /// <summary>
    /// A class used to output log messages to stream objects.
    /// </summary>
    public class StreamLoggerOutput :
        ILoggerOutput<Stream>
    {
        private Encoding m_encodingToUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Output.StreamLoggerOutput" /> class.
        /// </summary>
        public StreamLoggerOutput()
        {
            m_encodingToUse = Constants.DefaultEncoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Output.StreamLoggerOutput" /> class.
        /// </summary>
        /// <param name="encoding">Encoding to use on output.</param>
        public StreamLoggerOutput(Encoding encoding)
        {
            m_encodingToUse = Constants.DefaultEncoding;
            EncodingToUse = encoding;
        }

        /// <summary>
        /// Interacts with the system using the given output target,
        /// while using the given log message a parameter.
        /// </summary>
        /// <param name="logMessage">Log's fully constructed message, including all wrapping formats.</param>
        /// <param name="outputTarget">Some output target for the log message,
        /// possibly a stream or a file's path.</param>
        public void OutputLog(string logMessage, Stream outputTarget)
        {
            if (logMessage == null)
            {
                throw new ArgumentNullException(nameof(logMessage), "Message to log can't be null");
            }

            if (string.IsNullOrEmpty(logMessage))
            {
                throw new ArgumentException("Message to log can't be empty", nameof(logMessage));
            }

            if (outputTarget == null)
            {
                throw new ArgumentNullException(nameof(outputTarget), "Stream to log to can't be null");
            }

            if (!outputTarget.CanWrite)
            {
                throw new ArgumentException("Given stream has no writing abilities", nameof(outputTarget));
            }

            var streamWriter = new StreamWriter(outputTarget, EncodingToUse);
            streamWriter.AutoFlush = true;
            streamWriter.WriteLine(logMessage);
        }

        public async Task OutputLogAsync(string logMessage, Stream outputTarget)
        {
            if (logMessage == null)
            {
                throw new ArgumentNullException(nameof(logMessage), "Message to log can't be null");
            }

            if (string.IsNullOrEmpty(logMessage))
            {
                throw new ArgumentException("Message to log can't be empty", nameof(logMessage));
            }

            if (outputTarget == null)
            {
                throw new ArgumentNullException(nameof(outputTarget), "Stream to log to can't be null");
            }

            if (!outputTarget.CanWrite)
            {
                throw new ArgumentException("Given stream has no writing abilities", nameof(outputTarget));
            }

            using (var streamWriter = new StreamWriter(outputTarget, EncodingToUse))
            {
                streamWriter.AutoFlush = true;
                await streamWriter.WriteLineAsync(logMessage);
            }
        }

        /// <summary>
        /// Gets or sets the encoding used to output the log message.
        /// </summary>
        public Encoding EncodingToUse
        {
            get => m_encodingToUse;
            set => m_encodingToUse = value;
        }
    }
}