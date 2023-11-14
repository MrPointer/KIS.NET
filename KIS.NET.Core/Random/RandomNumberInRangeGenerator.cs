using System;

namespace KIS.NET.Core.Random
{
    /// <summary>
    /// A class used to generate random numbers that fall in a defined range. <br />
    /// For example: Given the range of 1-20, 13 is a valid number but 21 isn't.
    /// </summary>
    public class RandomNumberInRangeGenerator : IRandomNumberGenerator
    {
        private static readonly System.Random srm_RandomGenerator;

        /// <inheritdoc />
        public int GenerateValue<TBase>(TBase randomBase = default)
        {
            if (randomBase == null)
                throw new ArgumentNullException(nameof(randomBase),
                    "Random base can't be null in this type of generator - must be a range of integers.");
            if (!(randomBase is int[] numArray))
                throw new ArgumentException("Parameter for this method must be an array of ints - int[]");
            if (numArray.Length != 2)
                throw new ArgumentException(
                    "Random base range must consist of exactly 2 values - Range start and range end",
                    nameof(randomBase));
            return srm_RandomGenerator.Next(numArray[0], numArray[1]);
        }

        static RandomNumberInRangeGenerator()
        {
            srm_RandomGenerator = new System.Random();
        }
    }
}