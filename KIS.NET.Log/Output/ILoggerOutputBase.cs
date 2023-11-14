using System.Text;

namespace KIS.NET.Log.Output
{
    /// <summary>
    /// A base interface declaring properties essential for the output process. <br />
    /// As this is a *Base* interface, it should only be inherited by other interfaces,
    /// whereas the concrete types should be implemented instead.
    /// </summary>
    public interface ILoggerOutputBase
    {
        /// <summary>
        /// Gets or sets the encoding used to output the log message.
        /// </summary>
        Encoding EncodingToUse { get; set; }
    }
}