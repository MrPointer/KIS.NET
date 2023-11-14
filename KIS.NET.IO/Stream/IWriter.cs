namespace KIS.NET.IO.Stream
{
    /// <summary>
    /// An interface declaring methods to write data to an output stream in a custom manner.
    /// </summary>
    /// <typeparam name="TInput">Type of input data to send.</typeparam>
    public interface IWriter<in TInput>
    {
        /// <summary>Writes the given data to the given stream.</summary>
        /// <param name="outputStream">Reference to an output stream.</param>
        /// <param name="data">Data to send.</param>
        void Write(System.IO.Stream outputStream, TInput data);
    }
}