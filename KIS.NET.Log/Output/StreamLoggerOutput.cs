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
        ILoggerOutput<Stream>,
        ISimpleLoggerOutput<Stream>,
        ILoggerOutputBase,
        IAsyncLoggerOutput<Stream>
    {
        private Encoding m_EncodingToUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Output.StreamLoggerOutput" /> class.
        /// </summary>
        public StreamLoggerOutput()
        {
            m_EncodingToUse = Constants.DefaultEncoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Output.StreamLoggerOutput" /> class.
        /// </summary>
        /// <param name="i_Encoding">Encoding to use on output.</param>
        public StreamLoggerOutput(Encoding i_Encoding)
        {
            m_EncodingToUse = Constants.DefaultEncoding;
            EncodingToUse = i_Encoding;
        }

        /// <summary>
        /// Interacts with the system using the given output target,
        /// while using the given log message a parameter.
        /// </summary>
        /// <param name="i_LogMessage">Log's fully constructed message, including all wrapping formats.</param>
        /// <param name="i_OutputTarget">Some output target for the log message,
        /// possibly a stream or a file's path.</param>
        public void OutputLog(string i_LogMessage, Stream i_OutputTarget)
        {
            if (i_LogMessage == null)
            {
                throw new ArgumentNullException(nameof(i_LogMessage), "Message to log can't be null");
            }

            if (string.IsNullOrEmpty(i_LogMessage))
            {
                throw new ArgumentException("Message to log can't be empty", nameof(i_LogMessage));
            }

            if (i_OutputTarget == null)
            {
                throw new ArgumentNullException(nameof(i_OutputTarget), "Stream to log to can't be null");
            }

            if (!i_OutputTarget.CanWrite)
            {
                throw new ArgumentException("Given stream has no writing abilities", nameof(i_OutputTarget));
            }

            var streamWriter = new StreamWriter(i_OutputTarget, EncodingToUse);
            streamWriter.AutoFlush = true;
            streamWriter.WriteLine(i_LogMessage);
        }


        public Task OutputLogAsync(string logMessage, Stream outputTarget)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the encoding used to output the log message.
        /// </summary>
        public Encoding EncodingToUse
        {
            get => m_EncodingToUse;
            set => m_EncodingToUse = value;
        }
    }
}