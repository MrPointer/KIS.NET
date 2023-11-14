namespace KIS.NET.IO.Stream
{
    /// <summary>
    /// An interface declaring methods to clone one stream to another, without affecting the copied stream's status.
    /// </summary>
    /// <typeparam name="TLimit">Type that defines the limits of the stream to copy.</typeparam>
    public interface IStreamCloner<in TLimit>
    {
        /// <summary>Clones the whole given stream to a new stream object.</summary>
        /// <param name="originalStream">Reference to a stream object to clone</param>
        /// <returns>Reference to the cloned stream object.</returns>
        System.IO.Stream Clone(System.IO.Stream originalStream);

        /// <summary>
        /// Clones the given stream to a new stream object, from its' beginning to the given position.
        /// </summary>
        /// <param name="originalStream">Reference to a stream object to clone</param>
        /// <param name="endPosition">Final position of the original stream at the clone process.</param>
        /// <returns>Reference to the cloned stream object.</returns>
        System.IO.Stream Clone(System.IO.Stream originalStream, TLimit endPosition);

        /// <summary>
        /// Clones the given stream to a new stream object, from the given begin position to the given end position.
        /// </summary>
        /// <param name="originalStream">Reference to a stream object to clone</param>
        /// <param name="beginPosition">Initial position of the original stream at the clone process.</param>
        /// <param name="endPosition">Final position of the original stream at the clone process.</param>
        /// <returns>Reference to the cloned stream object.</returns>
        System.IO.Stream Clone(System.IO.Stream originalStream, TLimit beginPosition, TLimit endPosition);
    }
}