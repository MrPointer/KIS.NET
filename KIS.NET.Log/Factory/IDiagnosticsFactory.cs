using System;
using System.Diagnostics;

namespace KIS.NET.Log.Factory
{
    /// <summary>
    /// An interface declaring methods to create diagnostics-related concrete types.
    /// </summary>
    public interface IDiagnosticsFactory
    {
        /// <summary>
        /// Creates a <see cref="T:System.Diagnostics.StackTrace" /> object based on the given parameters.
        /// </summary>
        /// <param name="wrappedException">Reference to the exception
        /// that the StackTrace will wrap.</param>
        /// <param name="fileInfoRequired">Boolean value indicating weather
        /// the file info of the wrapped exception is required.</param>
        /// <returns>Reference to the created StackTrace object.</returns>
        StackTrace MakeStackTrace(Exception wrappedException, bool fileInfoRequired);
    }
}