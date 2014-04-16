using JsonProcessing;
using MediaHandleUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaHandleConsole
{
	class Program
	{
		#region Fields
		
		private static readonly List<string> _movieFileExtensions = new List<string>
		{
			".mkv",
			".avi",
			".mpg",
			".wmv",
			".mp4"
		};

		private static readonly List<string> _videoDisplayResolutions = new List<string>
		{
			"480p",
			"720p",
			"1080p"
		};

		#endregion Fields

		static void Main(string[] args)
		{
			Program prog = new Program();

			List<FileInfo> allMovieFiles = prog.GetMovieFiles();

			IEnumerable<FileInfo> sampleFiles = GetProbableSampleFiles(allMovieFiles);

			IEnumerable<FileInfo> wantedMovieFiles = allMovieFiles.Except(sampleFiles);

			prog.TestJson();
		}

		private List<FileInfo> GetMovieFiles()
		{
			DirectoryInfo moviesDirectoryInfo = new DirectoryInfo(@"\\SERVER\Downloads\Movies\");

			IEnumerable<FileInfo> allFiles = moviesDirectoryInfo.GetFiles("*", SearchOption.AllDirectories);
			
			return allFiles.Where(f => _movieFileExtensions.Any(f.Extension.Equals)).ToList();
		}

		private static IEnumerable<FileInfo> GetProbableSampleFiles(IEnumerable<FileInfo> movieFiles)
		{
			return movieFiles.Where(f => f.Name.Contains("sample", StringComparison.OrdinalIgnoreCase)).Where(i => (i.Length / 1024 / 1024) < 50 );
		}

		private void TestJson()
		{
			try
			{
				string searchRequest = JsonRequest.CreateRequest("The Matrix Revisited");

				MovieSearchAggregate searchResponse = JsonRequest.MakeRequest(searchRequest);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.Read();
			}
		}
	}
}
