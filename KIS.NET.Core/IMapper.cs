namespace KIS.NET.Core
{
    /// <summary>
    /// An interface declaring methods to map objects to an output-friendly representation of them.
    /// </summary>
    /// <typeparam name="TInput">Type of the object to map.</typeparam>
    /// <typeparam name="TOutput">Type of the map's outcome.</typeparam>
    public interface IMapper<TInput, TOutput>
    {
        /// <summary>
        /// Maps an output-friendly object back to a "work" object.
        /// </summary>
        /// <param name="mappedObject">Reference to the mapped object.</param>
        /// <returns>Reference to the re-mapped object.</returns>
        TInput MapFrom(TOutput mappedObject);

        /// <summary>
        /// Maps an object to an output-friendly representation of it.
        /// </summary>
        /// <param name="objectToMap">Reference to the object to map.</param>
        /// <returns>Reference to the mapped object.</returns>
        TOutput MapTo(TInput objectToMap);
    }
}