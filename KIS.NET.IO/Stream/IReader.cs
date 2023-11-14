namespace KIS.NET.IO.Stream
{
    /// <summary>
    /// An interface delcaring methods used to read input streams in a custom manner.
    /// </summary>
    /// <typeparam name="TOutput">Type of the output returned from a read operation.</typeparam>
    public interface IReader<out TOutput>
    {
        TOutput Read(System.IO.Stream inputStream);
    }
}