namespace KIS.NET.IO.Stream
{
    /// <summary>
    /// An interface declaring methods used to truncate data that's being written to a stream,
    /// so instead of writing a whole buffer at once, it will be written as separated fragments.
    /// </summary>
    public interface ITruncator
    {
        /// <summary>
        /// Truncates a given array of bytes to fragments, writing them instead of the whole buffer at once.
        /// </summary>
        /// <param name="outputStream">Reference to the stream we're writing to.</param>
        /// <param name="dataToTruncate">Reference to an array of bytes storing the data to truncate and write.</param>
        void Truncate(System.IO.Stream outputStream, byte[] dataToTruncate);

        /// <summary>
        /// Truncates a given stream to fragments, writing them instead of the whole buffer at once.
        /// </summary>
        /// <param name="outputStream">Reference to the stream we're writing to.</param>
        /// <param name="inputStream">Reference to the stream we're truncating.</param>
        void Truncate(System.IO.Stream outputStream, System.IO.Stream inputStream);
    }
}