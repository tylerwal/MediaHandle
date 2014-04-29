using FileProcessing;
using SearchProcessing;
using SearchProcessing.Constracts;
using SearchProcessing.TheMovieDb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaHandleConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			FileProcess fileProcess = new FileProcess(@"\\SERVER\Downloads\Movies\");

			List<FileInfo> allMovieFiles = fileProcess.GetMovieFiles();

			IEnumerable<FileInfo> sampleFiles = FileProcess.GetProbableSampleFiles(allMovieFiles);

			IEnumerable<FileInfo> wantedMovieFiles = allMovieFiles.Except(sampleFiles);
			
			TestJson();
		}
		
		private static void TestJson()
		{
			IRequest movieDbRequest = new TheMovieDbRequest();

			try
			{
				string searchRequest = movieDbRequest.CreateRequest("The Matrix Revisited");

				TheMovieDbRootResult searchResponse = RequestProcess.MakeRequest<TheMovieDbRootResult, TheMovieDbResult>(searchRequest);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.Read();
			}
		}

	}
}
