using System.Net;
using System.Net.Sockets;

namespace KIS.NET.IO.Network
{
    /// <summary>
    /// An interface declaring properties specific for network-based connections.
    /// </summary>
    public interface INetworkConnection : IConnection<EndPoint>
    {
        /// <summary>
        /// Gets a reference to the object used for the connection, which is a <see cref="T:System.Net.Sockets.Socket" />.
        /// </summary>
        Socket ConnectionObject { get; }

        /// <summary>
        /// Gets a boolean value indicating weather the network connection is currently connected or not.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>Gets the address type used by the network connection.</summary>
        AddressFamily AddressFamily { get; }

        /// <summary>Gets the socket type used by the network connection.</summary>
        SocketType SocketType { get; }

        /// <summary>
        /// Gets the protocol type used by the network connection.
        /// </summary>
        ProtocolType UsedProtocolType { get; }
    }
}