using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KIS.NET.Log.Output
{
    /// <summary>
    /// A class used to log messages to raw files on the file system.
    /// </summary>
    public class FileLoggerOutput :
        ILoggerOutput<string>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public FileLoggerOutput()
        {
            DefaultExtension = ".log";
            EncodingToUse = Constants.DefaultEncoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Output.FileLoggerOutput" /> class.
        /// </summary>
        /// <param name="encoding">Encoding to use on output.</param>
        public FileLoggerOutput(Encoding encoding)
        {
            DefaultExtension = ".log";
            EncodingToUse = encoding ?? Constants.DefaultEncoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Output.FileLoggerOutput" /> class.
        /// </summary>
        /// <param name="defaultExtension">Extension of the log file to apply if none has been passed.</param>
        public FileLoggerOutput(string defaultExtension)
        {
            DefaultExtension = ".log";
            if (defaultExtension == null)
            {
                throw new ArgumentNullException(nameof(defaultExtension), "Extension can't be null");
            }

            if (string.IsNullOrWhiteSpace(defaultExtension))
            {
                throw new ArgumentException("Extension can't be empty or contain only white spaces",
                    nameof(defaultExtension));
            }

            if (!Regex.IsMatch(defaultExtension, "^\\."))
            {
                throw new ArgumentException("Extension must be prefixed with a dot", nameof(defaultExtension));
            }

            DefaultExtension = defaultExtension;
            EncodingToUse = Constants.DefaultEncoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Output.FileLoggerOutput" /> class.
        /// </summary>
        /// <param name="defaultExtension">Extension of the log file to apply if none has been passed.</param>
        /// 
        ///             /// <param name="encoding">Encoding to use on output.</param>
        public FileLoggerOutput(string defaultExtension, Encoding encoding)
        {
            DefaultExtension = ".log";
            if (defaultExtension == null)
            {
                throw new ArgumentNullException(nameof(defaultExtension), "Extension can't be null");
            }

            if (string.IsNullOrWhiteSpace(defaultExtension))
            {
                throw new ArgumentException("Extension can't be empty or contain only white spaces",
                    nameof(defaultExtension));
            }

            if (!Regex.IsMatch(defaultExtension, "^\\."))
            {
                throw new ArgumentException("Extension must be prefixed with a dot", nameof(defaultExtension));
            }

            DefaultExtension = defaultExtension;
            EncodingToUse = encoding ?? Constants.DefaultEncoding;
        }

        /// <summary>
        /// Interacts with the system using the given output target,
        /// while using the given log message a parameter.
        /// </summary>
        /// <param name="logMessage">Log's fully constructed message, including all wrapping formats.</param>
        /// <param name="outputTarget">Some output target for the log message,
        /// possibly a stream or a file's path.</param>
        public void OutputLog(string logMessage, string outputTarget)
        {
            if (logMessage == null)
            {
                throw new ArgumentNullException(nameof(logMessage), "Log message can't be null");
            }

            if (string.IsNullOrEmpty(logMessage))
            {
                throw new ArgumentException("Log message can't be empty", nameof(logMessage));
            }

            if (outputTarget == null)
            {
                throw new ArgumentNullException(nameof(outputTarget), "Given file path can't be null");
            }

            if (string.IsNullOrWhiteSpace(outputTarget))
            {
                throw new ArgumentException("Given file path can't be empty or contain only white spaces",
                    nameof(outputTarget));
            }

            string directoryName = Path.GetDirectoryName(outputTarget);
            if (directoryName == null)
            {
                throw new ArgumentException("Given file path can't be the root directory");
            }

            Directory.CreateDirectory(directoryName);
            if (!Path.HasExtension(outputTarget))
            {
                outputTarget += DefaultExtension;
            }

            using (var fileStream = new FileStream(outputTarget, FileMode.Append, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(fileStream, EncodingToUse))
                {
                    streamWriter.WriteLine(logMessage);
                    streamWriter.Flush();
                }
            }
        }

        public async Task OutputLogAsync(string logMessage, string outputTarget)
        {
            if (logMessage == null)
            {
                throw new ArgumentNullException(nameof(logMessage), "Log message can't be null");
            }

            if (string.IsNullOrEmpty(logMessage))
            {
                throw new ArgumentException("Log message can't be empty", nameof(logMessage));
            }

            if (outputTarget == null)
            {
                throw new ArgumentNullException(nameof(outputTarget), "Given file path can't be null");
            }

            if (string.IsNullOrWhiteSpace(outputTarget))
            {
                throw new ArgumentException("Given file path can't be empty or contain only white spaces",
                    nameof(outputTarget));
            }

            string parentDirectoryName = Path.GetDirectoryName(outputTarget);
            if (parentDirectoryName == null)
            {
                throw new ArgumentException("Given file path can't be the root directory");
            }

            Directory.CreateDirectory(parentDirectoryName);

            bool isExtensionProvided = Path.HasExtension(outputTarget);
            if (!isExtensionProvided)
            {
                outputTarget += DefaultExtension;
            }

            using (var fileStream = new FileStream(outputTarget, FileMode.Append, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(fileStream, EncodingToUse))
                {
                    await streamWriter.WriteLineAsync(logMessage);
                    await streamWriter.FlushAsync();
                }
            }
        }

        /// <summary>
        /// Gets or sets the encoding used to output the log message.
        /// </summary>
        public Encoding EncodingToUse { get; set; }

        /// <summary>
        /// Gets or sets the default extension to append to the logged file,
        /// in case none was provided as part of the file's path.
        /// </summary>
        public string DefaultExtension { get; }
    }
}