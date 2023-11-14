using System;
using System.Diagnostics;

namespace KIS.NET.Log.Factory
{
    /// <summary>
    /// A class used as a factory to create concrete <see cref="T:System.Diagnostics.StackTrace" /> objects.
    /// </summary>
    public class DiagnosticsFactory : IDiagnosticsFactory
    {
        /// <inheritdoc />
        public StackTrace MakeStackTrace(Exception wrappedException, bool fileInfoRequired)
        {
            return wrappedException == null
                ? new StackTrace(fileInfoRequired)
                : new StackTrace(wrappedException, fileInfoRequired);
        }
    }
}