using System;

namespace KIS.NET.IO.Network
{
    public class Constants
    {
        internal const int MaximumNetBufferSize = 1024;

        /// <summary>Gets the max sane value for a connection timeout.</summary>
        internal static TimeSpan MaximumSaneConnectionTimeout;

        internal const int TimeoutErrorCode = 10200;

        static Constants()
        {
            MaximumSaneConnectionTimeout = TimeSpan.FromMinutes(5.0);
        }
    }
}