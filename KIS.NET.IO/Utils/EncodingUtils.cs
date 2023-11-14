using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KIS.NET.IO.Utils
{
    /// <summary>
    /// A static class defining various extension methods regarding encoding.
    /// </summary>
    public static class EncodingExtensions
    {
        /// <summary>
        /// Checks weather the given list of bytes equals to the preamble of the provided encoding.
        /// </summary>
        /// <param name="checkedData">Reference to a list of bytes to check.</param>
        /// <param name="encoding">Reference to an encoding object providing the necessary preamble information.</param>
        /// <returns>True if the checked data is the encoding's preamble, false otherwise.</returns>
        public static bool? EqualsToEncodingPreamble(
            this IList<byte> checkedData,
            Encoding encoding)
        {
            if (checkedData == null)
            {
                throw new ArgumentNullException(nameof(checkedData), "Checked data can't be null");
            }

            byte[] preamble = encoding.GetPreamble();
            return preamble.Length == 0
                ? new bool?()
                : new bool?(checkedData.SequenceEqual<byte>((IEnumerable<byte>)preamble));
        }
    }
}