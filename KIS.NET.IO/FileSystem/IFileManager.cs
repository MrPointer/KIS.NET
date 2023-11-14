using System.Collections.Generic;
using KIS.NET.Core;

namespace KIS.NET.IO.FileSystem
{
    /// <summary>
    /// An interface declaring methods to wrap IO operations in a manager class.
    /// </summary>
    /// <typeparam name="TInput">Type of the original object to save.</typeparam>
    /// <typeparam name="TOutput">Type of the output-friendly representation of the object to save.</typeparam>
    public interface IFileManager<TInput, TOutput>
    {
        /// <summary>
        /// Loads a file with the given path or the default file if none is given, and maps file's data to a matching object.
        /// </summary>
        /// <param name="objectMapper">Reference to a mapper used to map an output-friendly
        /// representation of some object back to its' original form.</param>
        /// <param name="filePath">Path of the file to load. Can be null if default should be used.</param>
        /// <returns>An object mapped from the loaded data.</returns>
        TInput LoadFile(IMapper<TInput, TOutput> objectMapper, string filePath = null);

        /// <summary>
        /// Loads a file with the given path or the default file if none is given, and maps file's data to a collection of matching objects.
        /// </summary>
        /// <param name="objectMapper">Reference to a mapper used to map an output-friendly
        /// representation of some object back to its' original form.</param>
        /// <param name="filePath">Path of the file to load. Can be null if default should be used.</param>
        /// <returns>A collection of objects mapped from the loaded data.</returns>
        IEnumerable<TInput> LoadFiles(IMapper<TInput, TOutput> objectMapper, string filePath = null);

        /// <summary>
        /// Saves a file with the given path or the default file if none is given, using the given object's data.
        /// </summary>
        /// <param name="objectMapper">Reference to a mapper used to map an object to an output-friendly representation of it.</param>
        /// <param name="objectToMap">Reference to the object to map.</param>
        /// <param name="filePath">Path of the file to save. Can be null if default should be used.</param>
        void SaveFile(IMapper<TInput, TOutput> objectMapper, TInput objectToMap, string filePath = null);
    }
}