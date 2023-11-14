using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace KIS.NET.IO.Network
{
    /// <summary>A class representing a TCP-based protocol connection.</summary>
    public class TcpConnection : INetworkConnection
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:KIS.NET.IO.Network.TcpConnection" /> class, based on the given address type.
        /// </summary>
        /// <param name="addressFamily">Address type to use with this connection instance.</param>
        public TcpConnection(AddressFamily addressFamily)
        {
            SocketType = SocketType.Stream;
            UsedProtocolType = ProtocolType.Tcp;
            ConnectionObject = new Socket(addressFamily, SocketType, ProtocolType.Tcp);
            AsyncLockObject = new object();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ConnectionObject?.Dispose();
        }

        /// <summary>Attempts to connect to the given end point.</summary>
        /// <param name="endPoint">Reference to an end point object, possibly null if implementation is always default.</param>
        /// <returns>True if connected successfully, false otherwise.</returns>
        public bool Connect(EndPoint endPoint)
        {
            if (endPoint == null)
            {
                throw new ArgumentNullException(nameof(endPoint), "Endpoint can't be null");
            }

            ConnectionObject.Connect(endPoint);
            IsConnected = ConnectionObject.Connected;
            return IsConnected;
        }

        /// <inheritdoc />
        public bool Connect(EndPoint endPoint, double timeout)
        {
            return Connect(endPoint, TimeSpan.FromMilliseconds(timeout));
        }

        /// <inheritdoc />
        public bool Connect(EndPoint endPoint, TimeSpan timeout)
        {
            if (endPoint == null)
            {
                throw new ArgumentNullException(nameof(endPoint), "Endpoint can't be null");
            }

            if (timeout == TimeSpan.Zero)
            {
                throw new ArgumentException("Timeout can't be zero - No connection is ever made this fast",
                    nameof(timeout));
            }

            if (timeout < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout), "Timeout cant be set to a negative value.");
            }

            if (timeout > Constants.MaximumSaneConnectionTimeout)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout),
                    "Timeout must be less than the maximum sane timeout value, which is " +
                    Constants.MaximumSaneConnectionTimeout + " minutes");
            }

            object asyncLockObject = AsyncLockObject;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(asyncLockObject, ref lockTaken);
                if (ConnectionObject.BeginConnect(endPoint, null, null).AsyncWaitHandle
                    .WaitOne(timeout, true))
                {
                    IsConnected = ConnectionObject.Connected;
                    return IsConnected;
                }

                ConnectionObject.Close();
                throw new SocketException(10200);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(asyncLockObject);
                }
            }
        }

        /// <summary>
        /// Receives data from the connected end point.
        /// A call to the <see cref="M:KIS.NET.IO.Network.IConnection`1.Connect(`0)" /> method must be made before calling this method.
        /// <para />
        /// Blocks until any data has been read/received from the end point.
        /// </summary>
        /// <returns>An array of bytes storing the received data.</returns>
        /// <exception cref="T:System.InvalidOperationException">If method has been called before calling
        /// the <see cref="M:KIS.NET.IO.Network.IConnection`1.Connect(`0)" /> method successfully.</exception>
        public byte[] ReceiveData()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException(
                    "ConnectionObject is not connected, please make sure a successful call has been made to the Connect() method.");
            }

            byte[] buffer = new byte[1024];
            do
            {
            } while (ConnectionObject.Receive(buffer) > 0);

            return buffer;
        }

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
        public bool SendData(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Data to send can't be null");
            }

            if (!IsConnected)
            {
                throw new InvalidOperationException(
                    "ConnectionObject is not connected, please make sure a successful call has been made to the Connect() method.");
            }

            int length = data.Length;
            int offset = 0;
            int size = 0;
            while (offset <= length)
            {
                size = length - size;
                if (size < 1024)
                {
                    offset += ConnectionObject.Send(data, offset, size, SocketFlags.None);
                }
                else
                {
                    offset += ConnectionObject.Send(data, offset, 1024, SocketFlags.None);
                }
            }

            return offset == length;
        }

        /// <summary>
        /// Gets a reference to the object used for the connection, which is a <see cref="T:System.Net.Sockets.Socket" />.
        /// </summary>
        public Socket ConnectionObject { get; }

        /// <summary>
        /// Gets a boolean value indicating weather the network connection is currently connected or not.
        /// </summary>
        public bool IsConnected { get; private set; }

        public AddressFamily AddressFamily { get; }

        /// <summary>Gets the socket type used by the network connection.</summary>
        public SocketType SocketType { get; }

        /// <summary>
        /// Gets the protocol type used by the network connection.
        /// </summary>
        public ProtocolType UsedProtocolType { get; }

        /// <inheritdoc />
        public object AsyncLockObject { get; }
    }
}