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

		public static string ComputeMovieHash(string filePath)
		{
			byte[] result;
			using (Stream input = File.OpenRead(filePath))
			{
				result = ComputeMovieHash(input);
			}
			
			// convert the result to a string representation
			return BitConverter.ToString(result).Replace("-", string.Empty);
		}

		private static byte[] ComputeMovieHash(Stream input)
		{
			long streamSize = input.Length;
			long lhash = streamSize;
			byte[] buffer = new byte[sizeof(long)];

			lhash = ReadFile(input, buffer, lhash);

			// if file size is small enough, start at beginning position again; otherwise, read end of file
			input.Position = Math.Max(0, streamSize - _sixteenBitWordSize);

			lhash = ReadFile(input, buffer, lhash);
			
			// I think the next line is not necessary because of the 'using' above
			//input.Close();

			byte[] result = BitConverter.GetBytes(lhash);
			Array.Reverse(result);

			return result;
		}

		private static long ReadFile(Stream input,  byte[] buffer, long lhash)
		{
			long increment = 0;

			while (increment < (_sixteenBitWordSize / _longSize)
				&& (input.Read(buffer, 0, _longSize) > 0))
			{
				increment++;
				lhash += BitConverter.ToInt64(buffer, 0);
			}

			return lhash;
		}
	}

}