using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MediaHandleUtilities
{
	/// <summary>
	/// Source code taken from: http://trac.opensubtitles.org/projects/opensubtitles/wiki/HashSourceCodes
	/// </summary>
	public static class HashUtility
	{
		public static string ComputeMovieHash(string filename)
		{
			byte[] result;
			using (Stream input = File.OpenRead(filename))
			{
				result = ComputeMovieHash(input);
			}
			return ToHexadecimal(result);
		}

		private static byte[] ComputeMovieHash(Stream input)
		{
			long streamsize = input.Length;
			long lhash = streamsize;

			long i = 0;
			byte[] buffer = new byte[sizeof(long)];
			while (i < 65536 / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
			{
				i++;
				lhash += BitConverter.ToInt64(buffer, 0);
			}

			input.Position = Math.Max(0, streamsize - 65536);
			i = 0;
			while (i < 65536 / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
			{
				i++;
				lhash += BitConverter.ToInt64(buffer, 0);
			}
			input.Close();
			byte[] result = BitConverter.GetBytes(lhash);
			Array.Reverse(result);
			return result;
		}

		private static string ToHexadecimal(IEnumerable<byte> bytes)
		{
			StringBuilder hexBuilder = new StringBuilder();

			foreach (byte b in bytes)
			{
				hexBuilder.Append(b.ToString("x2"));
			}

			return hexBuilder.ToString().ToUpper();
		}
	}

}