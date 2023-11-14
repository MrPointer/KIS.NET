using System;

namespace KIS.NET.IO.Network
{
    /// <summary>
    /// An interface declaring methods to manage some sort of I/O connection.
    /// <para />
    /// This is interface is also <see cref="T:System.IDisposable" />, thus can be used inside a 'using' block.
    /// </summary>
    public interface IConnection<in TEndPoint> : IDisposable
    {
        /// <summary>Attempts to connect to the given end point.</summary>
        /// <param name="endPoint">Reference to an end point object, possibly null if implementation is always default.</param>
        /// <returns>True if connected successfully, false otherwise.</returns>
        bool Connect(TEndPoint endPoint);

        /// <summary>
        /// Attempts to connect to the given end point for the given time.
        /// If fails, an exception is thrown.
        /// </summary>
        /// <param name="endPoint">Reference to an end point object, possibly null if implementation is always default.</param>
        /// <param name="timeout">Maximum time to wait on a connection before throwing an exception, in milliseconds.</param>
        /// <returns>True if connected successfully, false otherwise.</returns>
        bool Connect(TEndPoint endPoint, double timeout);

        /// <summary>
        /// Attempts to connect to the given end point for the given time.
        /// If fails, an exception is thrown.
        /// </summary>
        /// <param name="endPoint">Reference to an end point object, possibly null if implementation is always default.</param>
        /// <param name="timeout">Maximum time to wait on a connection before throwing an exception.</param>
        /// <returns>True if connected successfully, false otherwise.</returns>
        bool Connect(TEndPoint endPoint, TimeSpan timeout);

        /// <summary>
        /// Receives data from the connected end point.
        /// A call to the <see cref="M:KIS.NET.IO.Network.IConnection`1.Connect(`0)" /> method must be made before calling this method.
        /// <para />
        /// Blocks until any data has been read/received from the end point.
        /// </summary>
        /// <returns>An array of bytes storing the received data.</returns>
        /// <exception cref="T:System.InvalidOperationException">If method has been called before calling
        /// the <see cref="M:KIS.NET.IO.Network.IConnection`1.Connect(`0)" /> method successfully.</exception>
        byte[] ReceiveData();

        /// <summary>
        /// Sends the given data to the connected end point.
        /// A call to the <see cref="M:KIS.NET.IO.Network.IConnection`1.Connect(`0)" /> method must be made before calling this method.
        /// <para />
        /// Blocks until all data has been sent (and possibly acknowledged by the end point).
        /// </summary>
        /// <param name="data">An array of bytes representing the data to send.</param>
        /// <returns>True if data has been sent successfully, false otherwise.</returns>
        /// <exception cref="T:System.InvalidOperationException">If method has been called before calling
        /// the <see cref="M:KIS.NET.IO.Network.IConnection`1.Connect(`0)" /> method successfully.</exception>
        bool SendData(byte[] data);

        /// <summary>
        /// Gets a reference to an object used as the locking mechanism for asynchronous operations.
        /// </summary>
        object AsyncLockObject { get; }
    }
}