using System;
using System.Text;
using KIS.NET.Log.Factory;

namespace KIS.NET.Log.Format
{
    /// <summary>
    /// A class used to format exceptions using the current date time on top,
    /// exception's message and stack trace in the middle, an additional optional message in the bottom,
    /// and a separator line underneath them all.
    /// </summary>
    public class ExceptionWithDateTimeAndSeparatorFormat : ExceptionWithDateTimeFormat
    {
        private readonly char m_separatingChar;
        private readonly int m_numberOfSeparatingChars;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Format.ExceptionWithDateTimeAndSeparatorFormat" /> class.
        /// </summary>
        public ExceptionWithDateTimeAndSeparatorFormat(IDiagnosticsFactory diagnosticsFactory) : base(
            diagnosticsFactory)
        {
            m_separatingChar = '*';
            m_numberOfSeparatingChars = 15;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Format.ExceptionWithDateTimeAndSeparatorFormat" /> class.
        /// </summary>
        public ExceptionWithDateTimeAndSeparatorFormat(
            IDiagnosticsFactory diagnosticsFactory,
            string dateFormat,
            string timeFormat) : base(diagnosticsFactory, dateFormat, timeFormat)
        {
            m_separatingChar = '*';
            m_numberOfSeparatingChars = 15;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Format.ExceptionWithDateTimeAndSeparatorFormat" /> class.
        /// </summary>
        public ExceptionWithDateTimeAndSeparatorFormat(
            IDiagnosticsFactory diagnosticsFactory,
            char separatingChar,
            string dateFormat,
            string timeFormat) : base(diagnosticsFactory, dateFormat, timeFormat)
        {
            m_separatingChar = '*';
            m_numberOfSeparatingChars = 15;
            if (separatingChar <= char.MinValue)
            {
                return;
            }

            m_separatingChar = separatingChar;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:KIS.NET.Log.Format.ExceptionWithDateTimeAndSeparatorFormat" /> class.
        /// </summary>
        public ExceptionWithDateTimeAndSeparatorFormat(
            IDiagnosticsFactory diagnosticsFactory,
            char separatingChar,
            int numberOfSeparatingChars,
            string timeFormat,
            string dateFormat) : base(diagnosticsFactory, dateFormat, timeFormat)
        {
            m_separatingChar = '*';
            m_numberOfSeparatingChars = 15;
            if (separatingChar > char.MinValue)
            {
                m_separatingChar = separatingChar;
            }

            if (numberOfSeparatingChars == 0)
            {
                return;
            }

            m_numberOfSeparatingChars = numberOfSeparatingChars;
        }

        /// <summary>
        /// Formats the given exception according to the implementation-specific format constraints.
        /// </summary>
        /// <param name="objectToLog">Exception object to format.</param>
        /// <param name="additionalMessage">Additional message to append to the log.</param>
        /// <returns>Formatted log message.</returns>
        public override string Format(Exception objectToLog, string additionalMessage = null)
        {
            var stringBuilder = new StringBuilder(base.Format(objectToLog, additionalMessage));
            for (int index = 0; index < m_numberOfSeparatingChars; ++index)
                stringBuilder = stringBuilder.Append(m_separatingChar);
            return stringBuilder.AppendLine().ToString();
        }
    }
}