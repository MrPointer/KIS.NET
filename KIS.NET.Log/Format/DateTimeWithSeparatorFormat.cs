using System.Text;

namespace KIS.NET.Log.Format
{
    /// <summary>
    /// A class used to format log messages using a date time on top, message in the middle,
    /// and a separating-line at the bottom.
    /// </summary>
    public class DateTimeWithSeparatorFormat<T> : DateTimeFormat<T>
    {
        private readonly char m_separatingChar;
        private readonly int m_numberOfSeparatingChars;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public DateTimeWithSeparatorFormat()
        {
            m_separatingChar = '*';
            m_numberOfSeparatingChars = 15;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public DateTimeWithSeparatorFormat(char separatingChar)
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
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public DateTimeWithSeparatorFormat(
            char separatingChar,
            string dateFormat,
            string timeFormat) : base(dateFormat, timeFormat)
        {
            m_separatingChar = '*';
            m_numberOfSeparatingChars = 15;
            if (separatingChar <= char.MinValue)
            {
                return;
            }

            m_separatingChar = separatingChar;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public DateTimeWithSeparatorFormat(
            char separatingChar,
            int numberOfSeparatingChars,
            string dateFormat,
            string timeFormat) : base(dateFormat, timeFormat)
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
        /// Formats the given log message according to the implementation-specific format constraints.
        /// </summary>
        /// <param name="objectToLog">Log's message to format.</param>
        /// <param name="additionalMessage">Additional message to append to the log.</param>
        /// <returns>Formatted log message.</returns>
        public override string Format(T objectToLog, string additionalMessage = null)
        {
            var stringBuilder = new StringBuilder(base.Format(objectToLog, additionalMessage));
            for (int index = 0; index < m_numberOfSeparatingChars; ++index)
                stringBuilder = stringBuilder.Append(m_separatingChar);
            return stringBuilder.AppendLine().ToString();
        }
    }
}