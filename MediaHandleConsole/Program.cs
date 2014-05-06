using FileProcessing;
using MediaHandleDomain;
using SearchProcessing;
using SearchProcessing.Constracts;
using SearchProcessing.TheMovieDb;
using System;
using System.Collections.Generic;

namespace MediaHandleConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			//FileProcess fileProcess = new FileProcess(@"C:\Users\Tyler\Downloads\");

			//FileProcess fileProcess = new FileProcess(@"\\SERVER\Downloads\Movies\");

			FileProcess fileProcess = new FileProcess(@"Z:\Sort\");

			List<VideoFile> test = fileProcess.GetVideoFiles();
			
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
