namespace KIS.NET.Log
{
    /// <summary>
    /// A wrapper interface providing methods to log data of any type
    /// to an output target of any type, either synchronously ("Simple") or asynchronously. <br />
    /// This is utterly the most recommended interface to use, out of all available
    /// logger interfaces out there.
    /// </summary>
    /// <typeparam name="TLog">Type of the data to log.</typeparam>
    /// <typeparam name="TOutput">Type of the output target.</typeparam>
    public interface ILogger<TLog, TOutput> :
        ISimpleLogger<TLog, TOutput>,
        IAsyncLogger<TLog, TOutput>
    {
    }
}