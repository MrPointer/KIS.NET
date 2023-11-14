using System;
using System.Collections.Generic;
using System.Text;

namespace KIS.NET.IO.Stream
{
    /// <summary>
    /// A class used to write an <see cref="T:System.Collections.Generic.IEnumerable`1" /> of strings,
    /// each interpreted as a separate line, to a <see cref="T:System.IO.Stream" />.
    /// <para />
    /// The writer append <see cref="P:System.Environment.NewLine" /> to the end of each string to represent a newline.
    /// </summary>
    public class LineWriter : IWriter<IEnumerable<string>>
    {
        public LineWriter()
        {
            UsedEncoding = Encoding.Unicode;
        }

        public LineWriter(Encoding usedEncoding)
        {
            UsedEncoding = usedEncoding;
        }

        /// <inheritdoc />
        public void Write(System.IO.Stream outputStream, IEnumerable<string> data)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream), "Output stream can't be null");
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Data to send can't be null");
            }

            if (!outputStream.CanWrite)
            {
                throw new ArgumentException("Output stream must have writing permissions", nameof(outputStream));
            }

            foreach (string s in data)
            {
                byte[] bytes1 = UsedEncoding.GetBytes(s);
                byte[] bytes2 = UsedEncoding.GetBytes(Environment.NewLine);
                byte[] numArray = new byte[bytes1.Length + bytes2.Length];
                Array.Copy(bytes1, 0, numArray, 0, bytes1.Length);
                Array.Copy(bytes2, 0, numArray, bytes1.Length, bytes2.Length);
                outputStream.Write(numArray, 0, numArray.Length);
                outputStream.Flush();
            }
        }

        /// <summary>Gets the encoding used when writing data.</summary>
        public Encoding UsedEncoding { get; }
    }
}