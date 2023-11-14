using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using KIS.NET.Core;
using KIS.NET.Log.Factory;

namespace KIS.NET.Log
{
    /// <summary>
    /// A static utility class providing methods that wrap internal built-in loggers
    /// for easy-to-use logging purposes.
    /// </summary>
    public static class Log
    {
        /// <summary>Logs the given text to the console.</summary>
        /// <param name="text">String representing the text to log.</param>
        public static void LogText(string text)
        {
            LoggerFactory.MakeConsoleTextLogger()
                .Log(text, NullObject.Create());
        }

        /// <summary>
        /// Logs the given text to the given file on the file system.
        /// </summary>
        /// <param name="text">String representing the text to log.</param>
        /// <param name="outputFilePath">Path of the file to output the log to.</param>
        /// <param name="encoding"></param>
        public static void LogText(string text, string outputFilePath, Encoding encoding = null)
        {
            LoggerFactory.MakeLogger<string, string>(encoding)
                .Log(text, outputFilePath);
        }

        /// <summary>
        /// Logs the given text to the given file on the file system asynchronously.
        /// </summary>
        /// <param name="text">String representing the text to log.</param>
        /// <param name="outputFilePath">Path of the file to output the log to.</param>
        /// <param name="encoding"></param>
        public static async Task LogTextAsync(
            string text,
            string outputFilePath,
            Encoding encoding = null)
        {
            var logger = LoggerFactory.MakeLogger<string, string>(encoding);
            await logger.LogAsync(text, outputFilePath);
        }

        /// <summary>Logs the given text to the given stream object.</summary>
        /// <param name="text">String representing the text to log.</param>
        /// <param name="outputStream">Reference to a stream to output the log to.</param>
        /// <param name="encoding"></param>
        public static void LogText(string text, Stream outputStream, Encoding encoding = null)
        {
            LoggerFactory.MakeLogger<string, Stream>(encoding)
                .Log(text, outputStream);
        }

        /// <summary>
        /// Logs the given text to the given stream object asynchronously.
        /// </summary>
        /// <param name="text">String representing the text to log.</param>
        /// <param name="outputStream">Reference to a stream to output the log to.</param>
        /// <param name="encoding"></param>
        public static async Task LogTextAsync(string text, Stream outputStream, Encoding encoding = null)
        {
            var logger = LoggerFactory.MakeLogger<string, Stream>(encoding);
            await logger.LogAsync(text, outputStream);
        }

        /// <summary>Logs the given exception to the console.</summary>
        /// <param name="exception">Exception to be logged.</param>
        public static void LogException(Exception exception)
        {
            LoggerFactory.MakeConsoleExceptionLogger().Log(exception, NullObject.Create());
        }

        /// <summary>
        /// Logs the given exception to the given file on the file system.
        /// </summary>
        /// <param name="exception">Exception to be logged.</param>
        /// <param name="outputFilePath">Path of the file to output the log to.</param>
        /// <param name="encoding"></param>
        public static void LogException(
            Exception exception,
            string outputFilePath,
            Encoding encoding = null)
        {
            LoggerFactory.MakeLogger<Exception, string>(encoding).Log(exception, outputFilePath);
        }

        /// <summary>
        /// Logs the given exception to the given file on the file system asynchronously.
        /// </summary>
        /// <param name="exceptionToLog">Exception to be logged.</param>
        /// <param name="outputFilePath">Path of the file to output the log to.</param>
        /// <param name="encoding"></param>
        public static async Task LogExceptionAsync(
            Exception exceptionToLog,
            string outputFilePath,
            Encoding encoding = null)
        {
            var logger = LoggerFactory.MakeLogger<Exception, string>(encoding);
            await logger.LogAsync(exceptionToLog, outputFilePath);
        }

        /// <summary>Logs the given exception to the given stream object.</summary>
        /// <param name="exception">Exception to be logged.</param>
        /// <param name="outputStream">Reference to a stream to output the log to.</param>
        /// <param name="encoding"></param>
        public static void LogException(
            Exception exception,
            Stream outputStream,
            Encoding encoding = null)
        {
            LoggerFactory.MakeLogger<Exception, Stream>(encoding).Log(exception, outputStream);
        }

        /// <summary>
        /// Logs the given exception to the given stream object asynchronously.
        /// </summary>
        /// <param name="exceptionToLog">Exception to be logged.</param>
        /// <param name="outputStream">Reference to a stream to output the log to.</param>
        /// <param name="encoding"></param>
        public static async Task LogExceptionAsync(Exception exceptionToLog, Stream outputStream,
            Encoding encoding = null)
        {
            var logger = LoggerFactory.MakeLogger<Exception, Stream>(encoding);
            await logger.LogAsync(exceptionToLog, outputStream);
        }
    }
}