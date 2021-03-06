﻿using KIS.NET.Log.Format;
using KIS.NET.Log.Output;

namespace KIS.NET.Log
{
    /// <summary>
    /// An interface declaring methods to log data of any type
    /// to an output target of any type. <br />
    /// In this interface it is done synchronously, thus named "Simple".
    /// </summary>
    /// <typeparam name="TLog">Type of the object to log.</typeparam>
    /// <typeparam name="TOutput">Type of the log's output target.</typeparam>
    public interface ISimpleLogger<TLog, TOutput> : ILoggerBase<TOutput>
    {
        /// <summary>
        /// Logs the given object to the given output target,
        /// adding an optional additional message,and applying a custom format.
        /// </summary>
        /// <param name="objectToLog">Object to log.</param>
        /// <param name="outputTarget">Log's output target,
        ///     which could be a file, a stream, etc.</param>
        /// <param name="additionalMessage">
        ///     An additional message to append to the log message.</param>
        /// <param name="logFormat">
        ///     Reference to a formatter used to apply a custom format on the log message.
        ///     If none is given, the default one is used.</param>
        void Log(TLog objectToLog,
            TOutput outputTarget,
            string additionalMessage = null,
            ILogFormat<TLog> logFormat = null);

        /// <summary>
        /// Gets or sets a reference to an object used to interact with the system
        /// to perform the actual log operation.
        /// </summary>
        ISimpleLoggerOutput<TOutput> SimpleLoggerOutput { get; set; }
    }
}