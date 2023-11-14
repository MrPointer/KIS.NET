namespace KIS.NET.Core.Parse
{
    /// <summary>
    /// An interface declaring methods to parse some sort of input.
    /// </summary>
    /// <typeparam name="TInput">Type of the object that's being parsed.</typeparam>
    /// <typeparam name="TOutput">Type of the object that stores the parsed information.</typeparam>
    public interface IParser<in TInput, out TOutput> where TOutput : new()
    {
        /// <summary>Performs a parse operation on the given object.</summary>
        /// <param name="i_ParsableObject">Reference to an object that should be parsed.</param>
        /// <returns>Reference to an object that stores the parsing result.</returns>
        TOutput Parse(TInput i_ParsableObject);
    }
}