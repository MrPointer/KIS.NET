using System;
using System.Text;
using KIS.NET.Log.Factory;

namespace KIS.NET.Log.Format
{
    /// <summary>
    /// A class used to format exceptions using the current date time on top,
    /// exception's message and stack trace in the middle, and an additional optional message in the bottom.
    /// </summary>
    public class ExceptionWithDateTimeFormat : ExceptionFormat
    {
        private readonly string m_dateFormat;
        private readonly string m_timeFormat;

        /// <inheritdoc />
        public ExceptionWithDateTimeFormat(IDiagnosticsFactory diagnosticsFactory) : base(diagnosticsFactory)
        {
            m_dateFormat = "dd MMM yyyy";
            m_timeFormat = "HH:mm:ss";
        }

        /// <inheritdoc />
        public ExceptionWithDateTimeFormat(
            IDiagnosticsFactory diagnosticsFactory,
            string dateFormat,
            string timeFormat) : base(diagnosticsFactory)
        {
            m_dateFormat = "dd MMM yyyy";
            m_timeFormat = "HH:mm:ss";
            if (dateFormat != null)
            {
                m_dateFormat = dateFormat;
            }

            if (timeFormat == null)
            {
                return;
            }

            m_timeFormat = timeFormat;
        }

        /// <summary>
        /// Formats the given exception according to the implementation-specific format constraints.
        /// </summary>
        /// <param name="objectToLog">Exception object to format.</param>
        /// <param name="additionalMessage">Additional message to append to the log.</param>
        /// <returns>Formatted log message.</returns>
        public override string Format(Exception objectToLog, string additionalMessage = null)
        {
            string str = base.Format(objectToLog, additionalMessage);
            return new StringBuilder().Append(DateTime.Now.ToString(m_dateFormat + " " + m_timeFormat)).AppendLine()
                .AppendLine(str).ToString();
        }
    }
}