using KIS.NET.Log.Format;

namespace KIS.NET.Log
{
    /// <summary>
    /// A base interface declaring properties essential to the logging process. <br />
    /// As this is a *Base* interface, it should only be inherited by other interfaces,
    /// whereas the concrete types should be implemented instead.
    /// </summary>
    /// <typeparam name="TOutput">Type of the log's output target.</typeparam>
    public interface ILoggerBase<TOutput>
    {
        /// <summary>
        /// Gets or sets a reference to a message builder, used as the default builder
        /// if a custom format hasn't been provided.
        /// </summary>
        ILogMessageBuilder MessageBuilder { set; }
    }
}