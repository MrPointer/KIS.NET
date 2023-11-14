using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KIS.NET.IO.Utils;

namespace KIS.NET.IO.Stream
{
    /// <summary>
    /// A class used to read lines from the input stream,
    /// and return an <see cref="T:System.Collections.Generic.IEnumerable`1" /> containing them.
    /// <para/>
    /// This class assumes a new line starts on <see cref="P:System.Environment.NewLine" />.
    /// </summary>
    public class LineReader : IReader<IEnumerable<string>>
    {
        public LineReader()
        {
            UsedEncoding = Encoding.Unicode;
        }

        public LineReader(Encoding usedEncoding)
        {
            UsedEncoding = usedEncoding;
        }

        /// <summary>
        /// Checks weather the given list of bytes ends with a new line.
        /// </summary>
        /// <param name="checkedData">Reference to a list of bytes that will be checked.</param>
        /// <returns>True if the list of bytes contains an <see cref="P:System.Environment.NewLine" /> at the end, false otherwise.</returns>
        private bool EndsWithNewLine(IList<byte> checkedData)
        {
            if (checkedData == null)
            {
                throw new ArgumentNullException(nameof(checkedData), "Checked data can't be null");
            }

            byte[] bytes = UsedEncoding.GetBytes(Environment.NewLine);
            int length = bytes.Length;
            if (checkedData.Count < length)
            {
                return false;
            }

            var first = new List<byte>();
            for (int index = length; index > 0; --index)
                first.Add(checkedData[checkedData.Count - index]);
            return first.SequenceEqual(bytes);
        }

        /// <inheritdoc />
        public IEnumerable<string> Read(System.IO.Stream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream), "Input stream can't be null");
            }

            if (!inputStream.CanRead)
            {
                throw new ArgumentException("Input stream must have reading permissions", nameof(inputStream));
            }

            var lineBuffer = new List<byte>();
            bool isPreambleFound = false;

            while (inputStream.Position < inputStream.Length)
            {
                byte readByte = (byte)inputStream.ReadByte();
                lineBuffer.Add(readByte);

                if (!isPreambleFound)
                {
                    bool? isBufferPreamble = lineBuffer.EqualsToEncodingPreamble(UsedEncoding);
                    if (!isBufferPreamble.HasValue)
                    {
                        isPreambleFound = true;
                    }
                    else if (isBufferPreamble.Value)
                    {
                        isPreambleFound = true;
                        lineBuffer.Clear();
                    }
                }
                else if (EndsWithNewLine(lineBuffer))
                {
                    string lineAsString = UsedEncoding.GetString(lineBuffer.ToArray());
                    lineAsString = lineAsString.Remove(lineAsString.Length - Environment.NewLine.Length);
                    byte[] lineAsBytes = UsedEncoding.GetBytes(lineAsString);
                    byte[] convertedLineBytes = Encoding.Convert(UsedEncoding, Encoding.Unicode, lineAsBytes);
                    string convertedLine = Encoding.Unicode.GetString(convertedLineBytes);
                    lineBuffer.Clear();
                    yield return convertedLine;
                }
            }
        }

        /// <summary>Gets the encoding used by the reader.</summary>
        public Encoding UsedEncoding { get; }
    }
}