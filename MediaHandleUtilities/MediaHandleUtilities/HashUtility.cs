using System;
using System.IO;

namespace MediaHandleUtilities
{
	/// <summary>
	/// Source code taken from: http://trac.opensubtitles.org/projects/opensubtitles/wiki/HashSourceCodes
	/// </summary>
	public static class HashUtility
	{
		private const int _sixteenBitWordSize = 65536;
		private const int _longSize = sizeof(long);

		/// <summary>
		/// Computes the movie hash.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns>The hash string.</returns>
		public static string ComputeMovieHash(string filePath)
		{
			byte[] result;
			using (Stream input = File.OpenRead(filePath))
			{
				result = ComputeMovieHash(input);
			}
			
			// convert the result to a string representation
			return BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
		}

		/// <summary>
		/// Computes the movie hash.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>A byte array of the hash.</returns>
		private static byte[] ComputeMovieHash(Stream input)
		{
			long streamSize = input.Length;
			long hash = streamSize;
			byte[] buffer = new byte[_longSize];

			hash = ReadFile(input, buffer, hash);

			// if file size is small enough, start at beginning position again; otherwise, read end of file
			input.Position = Math.Max(0, streamSize - _sixteenBitWordSize);

			hash = ReadFile(input, buffer, hash);
			
			input.Close();

			// convert integer value of hash into array of bytes
			byte[] result = BitConverter.GetBytes(hash);
			Array.Reverse(result);

			return result;
		}

		/// <summary>
		/// Method that is ran twice during the Hash operation; once for the beginning of the file, once for the end of the
		/// file.  If the file is extremely short, this operation will act on the same byte stream.
		/// </summary>
		/// <param name="input">The file stream.</param>
		/// <param name="buffer">The buffer that holds the file data.</param>
		/// <param name="hash">The hash that is being generated.</param>
		/// <returns>The hash that is being generated.</returns>
		private static long ReadFile(Stream input,  byte[] buffer, long hash)
		{
			long increment = 0;

			while (increment < (_sixteenBitWordSize / _longSize)
				&& (input.Read(buffer, 0, _longSize) > 0))
			{
				increment++;
				hash += BitConverter.ToInt64(buffer, 0);
			}

			return hash;
		}
	}

}