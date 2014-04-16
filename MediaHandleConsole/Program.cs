using FileProcessing;
using JsonProcessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaHandleConsole
{
	class Program
	{
		#region Fields

		private const string _path = @"\\SERVER\Downloads\Movies\";
		
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

			List<FileInfo> allMovieFiles = FileProcess.GetMovieFiles(_path);

			IEnumerable<FileInfo> sampleFiles = FileProcess.GetProbableSampleFiles(allMovieFiles);

			IEnumerable<FileInfo> wantedMovieFiles = allMovieFiles.Except(sampleFiles);

			prog.TestJson();
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
