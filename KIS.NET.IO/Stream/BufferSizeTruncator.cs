using System;

namespace KIS.NET.IO.Stream
{
    /// <summary>
    /// A class used to truncate data by a set buffer size, meaning that written fragments
    /// will be at most at the size of the set buffer size.
    /// </summary>
    public class BufferSizeTruncator : ITruncator
    {
        public BufferSizeTruncator()
        {
            BufferSize = 4096;
        }

        public BufferSizeTruncator(int i_BufferSize)
        {
            BufferSize = i_BufferSize;
        }

        /// <inheritdoc cref="Truncate(System.IO.Stream,byte[])" />
        public void Truncate(System.IO.Stream outputStream, byte[] dataToTruncate)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream), "Output stream can't be null");
            }

            if (dataToTruncate == null)
            {
                throw new ArgumentNullException(nameof(dataToTruncate), "Data to truncate and write can't be null");
            }

            if (!outputStream.CanWrite)
            {
                throw new ArgumentException("Output stream must have writing permissions", nameof(outputStream));
            }

            int offset = 0;
            int length = dataToTruncate.Length;
            while (offset < length)
            {
                int count = length - offset;
                if (count < BufferSize)
                {
                    outputStream.Write(dataToTruncate, offset, count);
                    offset += count;
                }
                else
                {
                    outputStream.Write(dataToTruncate, offset, BufferSize);
                    offset += BufferSize;
                }
            }
        }

        /// <inheritdoc cref="Truncate(System.IO.Stream,System.IO.Stream)" />
        public void Truncate(System.IO.Stream outputStream, System.IO.Stream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream), "Source stream can't be null");
            }

            if (!inputStream.CanRead)
            {
                throw new ArgumentException("Input stream must have reading permissions", nameof(inputStream));
            }

            byte[] numArray = new byte[inputStream.Length];
            long position = inputStream.Position;
            inputStream.Position = 0L;
            int _ = inputStream.Read(numArray, 0, numArray.Length);
            Truncate(outputStream, numArray);
            inputStream.Position = position;
        }

        /// <summary>
        /// Gets the buffer size - Size of each written fragment. Default is 4096.
        /// </summary>
        public int BufferSize { get; private set; }
    }
}