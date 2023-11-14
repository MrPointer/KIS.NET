namespace KIS.NET.Log.Format
{
    /// <summary>
    /// An interface declaring methods to specify a format for the log message.
    /// </summary>
    /// <typeparam name="TFormatted">Type of the formatted object.</typeparam>
    public interface ILogFormat<in TFormatted>
    {
        /// <summary>
        /// Formats the given log message according to the implementation-specific format constraints.
        /// </summary>
        /// <param name="objectToLog">Reference to an object to format.</param>
        /// <param name="additionalMessage">Additional message to append to the log.</param>
        /// <returns>Formatted log message.</returns>
        string Format(TFormatted objectToLog, string additionalMessage = null);
    }
}