using System;
using System.Text;

namespace KIS.NET.Log.Format
{
    /// <summary>
    /// A class used to format log messages using a date time on top, message in the bottom.
    /// </summary>
    public class DateTimeFormat<T> : ILogFormat<T>
    {
        internal readonly string DateFormat;
        internal readonly string TimeFormat;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public DateTimeFormat()
        {
            DateFormat = "dd MMM yyyy";
            TimeFormat = "HH:mm:ss";
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public DateTimeFormat(string i_DateFormat, string i_TimeFormat)
        {
            DateFormat = "dd MMM yyyy";
            TimeFormat = "HH:mm:ss";
            if (i_DateFormat != null)
            {
                DateFormat = i_DateFormat;
            }

            if (i_TimeFormat == null)
            {
                return;
            }

            TimeFormat = i_TimeFormat;
        }

        /// <inheritdoc />
        public virtual string Format(T objectToLog, string additionalMessage = null)
        {
            if (objectToLog == null)
            {
                throw new ArgumentNullException(nameof(objectToLog), "Object to log can't be null");
            }

            var stringBuilder = new StringBuilder(DateTime.Now.ToString(DateFormat + " " + TimeFormat)).AppendLine()
                .Append("Message: ").Append(objectToLog).AppendLine();
            return string.IsNullOrEmpty(additionalMessage)
                ? stringBuilder.ToString()
                : stringBuilder.Append("Additional Message: ").AppendLine(additionalMessage).ToString();
        }
    }
}