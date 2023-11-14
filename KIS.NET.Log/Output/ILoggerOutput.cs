namespace KIS.NET.Log.Output
{
    /// <summary>
    /// A wrapper interface providing methods to log a message to an output target of some type,
    /// either synchronously ("Simple") or asynchronously. <br />
    /// This is utterly the most recommended interface to use, out of all available
    /// logger output interfaces out there.
    /// </summary>
    /// <typeparam name="T">Type of the output target.</typeparam>
    public interface ILoggerOutput<in T> :
        ISimpleLoggerOutput<T>,
        IAsyncLoggerOutput<T>
    {
    }
}