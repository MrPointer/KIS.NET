using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using KIS.NET.Core;
using KIS.NET.Log.Output;

namespace KIS.NET.Log.Factory
{
    /// <summary>
    /// A static factory class providing methods to create instances of various loggers,
    /// using a factory model. <br />
    /// The factory tries to optimize memory by using caching techniques as much as possible.
    /// </summary>
    public static class LoggerFactory
    {
        private static readonly IDictionary<Encoding, int> srm_FileLoggerEncodingRequests;
        private static readonly IDictionary<Encoding, int> srm_StreamLoggerEncodingRequests;
        private static TextLogger<NullObject> sm_consoleTextLogger;
        private static ExceptionLogger<NullObject> sm_consoleExceptionLogger;
        private static FileLoggerOutput sm_fileLoggerOutput;
        private static StreamLoggerOutput sm_streamLoggerOutput;

        static LoggerFactory()
        {
            srm_FileLoggerEncodingRequests = new Dictionary<Encoding, int>();
            srm_StreamLoggerEncodingRequests = new Dictionary<Encoding, int>();
        }

        /// <summary>
        /// Makes a <see cref="T:KIS.NET.Log.Output.ILoggerOutput`1" /> object based on the given encoding. <br />
        /// The method looks through the defined concrete classes implementing the interface
        /// to match the given type, and returns a possibly-cached instance of it.
        /// If no match is found, null is returned.
        /// </summary>
        /// <remarks>
        /// Use this method to get an instance of a complete implementation for some target type,
        /// to avoid the overhead of redefinition.
        /// <para>
        /// For example: The library defines an implementation for the <see cref="T:System.String" /> type,
        /// treating it as a file path which the log message should be written to. <br />
        /// </para>
        /// <br />
        /// <para>
        /// This method attempts to apply memoization techniques as much as possible
        /// to increase performance by avoiding the overhead
        /// of object creation for the same encoding. <br />
        /// After an instance has been firstly created, each call to this method with the same
        /// encoding will return the cached instance, yet a different encoding will create
        /// a new instance.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">Type of the logger's output target.</typeparam>
        /// <param name="encoding">Encoding to use in the log process.
        /// Affects caching behavior.</param>
        /// <returns>Reference to the created/retrieved object matching the given type,
        /// or null if no such instance could be generated.</returns>
        public static ILoggerOutput<T> MakeLoggerOutput<T>(Encoding encoding = null)
        {
            var type = typeof(T);
            if (type == typeof(string))
            {
                return (ILoggerOutput<T>)MakeFileLoggerOutput(encoding);
            }

            return type == typeof(Stream)
                ? (ILoggerOutput<T>)MakeStreamLoggerOutput(encoding)
                : null;
        }

        private static FileLoggerOutput MakeFileLoggerOutput(Encoding encoding = null)
        {
            encoding = encoding ?? Constants.DefaultEncoding;

            if (sm_fileLoggerOutput == null)
            {
                srm_FileLoggerEncodingRequests.Add(encoding, 1);
                sm_fileLoggerOutput = new FileLoggerOutput(encoding);
                return sm_fileLoggerOutput;
            }

            if (srm_FileLoggerEncodingRequests.ContainsKey(encoding))
            {
                if (srm_FileLoggerEncodingRequests.Count == 1 ||
                    ++srm_FileLoggerEncodingRequests[encoding] <= srm_FileLoggerEncodingRequests.Max(x => x.Value) ||
                    Equals(encoding, sm_fileLoggerOutput.EncodingToUse))
                {
                    return sm_fileLoggerOutput;
                }

                sm_fileLoggerOutput = new FileLoggerOutput(encoding);
                return sm_fileLoggerOutput;
            }

            srm_FileLoggerEncodingRequests.Add(encoding, 1);
            return new FileLoggerOutput(encoding);
        }

        private static StreamLoggerOutput MakeStreamLoggerOutput(Encoding encoding = null)
        {
            encoding = encoding ?? Constants.DefaultEncoding;

            if (sm_streamLoggerOutput == null)
            {
                srm_StreamLoggerEncodingRequests.Add(encoding, 1);
                sm_streamLoggerOutput = new StreamLoggerOutput(encoding);
                return sm_streamLoggerOutput;
            }

            if (srm_StreamLoggerEncodingRequests.ContainsKey(encoding))
            {
                if (srm_StreamLoggerEncodingRequests.Count == 1 ||
                    ++srm_StreamLoggerEncodingRequests[encoding] <=
                    srm_StreamLoggerEncodingRequests.Max(x => x.Value) ||
                    Equals(encoding, sm_streamLoggerOutput.EncodingToUse))
                {
                    return sm_streamLoggerOutput;
                }

                sm_streamLoggerOutput = new StreamLoggerOutput(encoding);
                return sm_streamLoggerOutput;
            }

            srm_StreamLoggerEncodingRequests.Add(encoding, 1);
            return new StreamLoggerOutput(encoding);
        }

        /// <summary>
        /// Makes a <see cref="T:KIS.NET.Log.ILogger`2" /> object based on the given
        /// log data type, output target type and encoding to use when logging. <br />
        /// This method assumes the given types have a built-in concrete implementation
        /// of the interface, otherwise it returns null.
        /// </summary>
        /// <typeparam name="TLog">Type of the data to log.</typeparam>
        /// <typeparam name="TOutput">Type of the output target.</typeparam>
        /// <param name="encoding">Encoding to use in the log process.</param>
        /// <returns>Reference to the created logger instance, or null if no concrete
        /// built-in implementation has been found.</returns>
        public static ILogger<TLog, TOutput> MakeLogger<TLog, TOutput>(Encoding encoding = null)
        {
            var type = typeof(TLog);
            var loggerOutput = MakeLoggerOutput<TOutput>(encoding);
            if (loggerOutput == null)
            {
                return null;
            }

            if (type == typeof(string))
            {
                return (ILogger<TLog, TOutput>)new TextLogger<TOutput>(loggerOutput);
            }

            return type == typeof(Exception)
                ? (ILogger<TLog, TOutput>)new ExceptionLogger<TOutput>(loggerOutput)
                : null;
        }

        /// <summary>
        /// Make a <see cref="T:KIS.NET.Log.TextLogger`1" /> object for <see cref="T:System.Console" /> output.
        /// </summary>
        /// <returns>Reference to the created text logger object.</returns>
        public static TextLogger<NullObject> MakeConsoleTextLogger()
        {
            return sm_consoleTextLogger ?? (sm_consoleTextLogger =
                new TextLogger<NullObject>(new ConsoleLoggerOutput()));
        }

        /// <summary>
        /// Make a <see cref="T:KIS.NET.Log.ExceptionLogger`1" /> object for <see cref="T:System.Console" /> output.
        /// </summary>
        /// <returns>Reference to the created exception logger object.</returns>
        public static ExceptionLogger<NullObject> MakeConsoleExceptionLogger()
        {
            return sm_consoleExceptionLogger ?? (sm_consoleExceptionLogger =
                new ExceptionLogger<NullObject>(new ConsoleLoggerOutput()));
        }

        /// <summary>
        /// Invalidates the cache by deleting all existing fields' references. <br />
        /// Internal method used only for testing purposes.
        /// </summary>
        internal static void InvalidateCache()
        {
            srm_FileLoggerEncodingRequests.Clear();
            srm_StreamLoggerEncodingRequests.Clear();
            sm_fileLoggerOutput = null;
            sm_streamLoggerOutput = null;
            GC.Collect();
        }
    }
}