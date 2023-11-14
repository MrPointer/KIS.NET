using System;
using System.Diagnostics;
using System.Text;
using KIS.NET.Log.Factory;

namespace KIS.NET.Log.Format
{
    /// <summary>
    /// A class used to format exceptions using the exception's
    /// message and stack trace, and an additional optional message.
    /// </summary>
    public class ExceptionFormat : ILogFormat<Exception>
    {
        private readonly IDiagnosticsFactory m_DiagnosticsFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Format.ExceptionFormat" /> class.
        /// </summary>
        public ExceptionFormat(IDiagnosticsFactory diagnosticsFactory)
        {
            if (diagnosticsFactory == null)
            {
                throw new ArgumentNullException(nameof(diagnosticsFactory), "Diagnostics factory can't be null");
            }

            m_DiagnosticsFactory = diagnosticsFactory;
        }

        /// <summary>
        /// Formats the given log message according to the implementation-specific format constraints.
        /// </summary>
        /// <param name="objectToLog">Reference to an object to format.</param>
        /// <param name="additionalMessage">Additional message to append to the log.</param>
        /// <returns>Formatted log message.</returns>
        public virtual string Format(Exception objectToLog, string additionalMessage = null)
        {
            if (objectToLog == null)
            {
                throw new ArgumentNullException(nameof(objectToLog), "Given exception to log can't be null");
            }

            var i_StringBuilder = new StringBuilder();
            BuildBasicExceptionInfo(ref i_StringBuilder, objectToLog);
            var i_ExceptionStackTrace = m_DiagnosticsFactory.MakeStackTrace(objectToLog, true);
            BuildAdvancedExceptionInfo(ref i_StringBuilder, i_ExceptionStackTrace);
            if (string.IsNullOrEmpty(additionalMessage))
            {
                return i_StringBuilder.ToString();
            }

            i_StringBuilder = i_StringBuilder.Append("Additional Message: ").Append(additionalMessage);
            i_StringBuilder = i_StringBuilder.AppendLine();
            return i_StringBuilder.ToString();
        }

        /// <summary>
        /// Builds a string encapsulating some basic exception information,
        /// such as its' concrete type and inner message.
        /// </summary>
        /// <param name="i_StringBuilder">Full referene to a StringBuilder object.</param>
        /// <param name="i_LoggedException">Reference to the logged exception.</param>
        private static void BuildBasicExceptionInfo(
            ref StringBuilder i_StringBuilder,
            Exception i_LoggedException)
        {
            i_StringBuilder = i_StringBuilder.Append("Type: ").Append(i_LoggedException.GetType());
            i_StringBuilder = i_StringBuilder.AppendLine();
            i_StringBuilder = i_StringBuilder.Append("Exception Message: ").Append(i_LoggedException.Message);
            i_StringBuilder = i_StringBuilder.AppendLine();
        }

        /// <summary>
        /// Builds a string encapsulating some advanced exception information,
        /// taken mostly from its' <see cref="T:System.Diagnostics.StackTrace" />.
        /// <para />
        /// The info includes the file name at which the exception occurred,
        /// the method name, the line number and the column number.
        /// </summary>
        /// <param name="stringBuilder">Full referene to a StringBuilder object.</param>
        /// <param name="exceptionStackTrace">Reference to the StackTrace object
        /// wrapped around the logged exception.</param>
        private static void BuildAdvancedExceptionInfo(
            ref StringBuilder stringBuilder,
            StackTrace exceptionStackTrace)
        {
            var frame = exceptionStackTrace.GetFrame(exceptionStackTrace.FrameCount - 1);
            stringBuilder = stringBuilder.Append("In: ").Append(frame.GetFileName());
            stringBuilder = stringBuilder.Append("; At: ").Append(frame.GetMethod().Name);
            int fileLineNumber = frame.GetFileLineNumber();
            int fileColumnNumber = frame.GetFileColumnNumber();
            if (fileLineNumber != 0)
            {
                stringBuilder = stringBuilder.Append("; Line: ").Append(fileLineNumber);
                if (fileColumnNumber != 0)
                {
                    stringBuilder = stringBuilder.Append("; Column: ").Append(fileColumnNumber);
                }
                else
                {
                    stringBuilder = stringBuilder.Append("; Column: Undefined");
                }
            }
            else
            {
                stringBuilder = stringBuilder.Append("; Line: Undefined;");
                stringBuilder = stringBuilder.Append(" Column: Undefined");
            }
        }
    }
}