using MediaHandleUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileProcessing
{
	public class FileProcess
	{
		private static readonly List<string> _movieFileExtensions = new List<string>
		{
			".mkv",
			".avi",
			".mpg",
			".wmv",
			".mp4"
		};

		public static List<FileInfo> GetMovieFiles(string path)
		{
			DirectoryInfo moviesDirectoryInfo = new DirectoryInfo(path);

			IEnumerable<FileInfo> allFiles = moviesDirectoryInfo.GetFiles("*", SearchOption.AllDirectories);

			return allFiles.Where(f => _movieFileExtensions.Any(f.Extension.Equals)).ToList();
		}

		public static IEnumerable<FileInfo> GetProbableSampleFiles(IEnumerable<FileInfo> movieFiles)
		{
			return movieFiles.Where(f => f.Name.Contains("sample", StringComparison.OrdinalIgnoreCase)).Where(i => (i.Length / 1024 / 1024) < 50);
		}
	}
}
