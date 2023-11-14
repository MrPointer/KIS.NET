namespace KIS.NET.Core.Random
{
    /// <summary>
    /// An interface declaring methods to generate random values.
    /// </summary>
    /// <typeparam name="TValue">Type of the value to be generated.</typeparam>
    public interface IRandomValueGenerator<out TValue>
    {
        /// <summary>
        /// Generates a random value using a custom algorithm that can be based on the given range predicate.
        /// </summary>
        /// <typeparam name="TBase">Type of a parameter the random function is based on (such as an integer range).</typeparam>
        /// <param name="randomBase">Some value that the random function will be based on, if any.
        /// This is an optional parameter.</param>
        /// <returns>The generated value.</returns>
        TValue GenerateValue<TBase>(TBase randomBase = default);
    }
}