﻿using FileProcessing;
using MediaHandleDomain;
using SearchProcessing;
using SearchProcessing.Constracts;
using SearchProcessing.TheMovieDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaHandleConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			FileProcess fileProcess = new FileProcess(@"C:\MediaHandleTest\");

			IEnumerable<VideoFile> videoFiles = fileProcess.GetVideoFiles();

			IRequest movieDbRequest = new TheMovieDbRequest();

			foreach (VideoFile videoFile in videoFiles)
			{
				try
				{
					string searchRequest = movieDbRequest.CreateSearchQuery(videoFile.Name);

					TheMovieDbRootResult searchResponse = RequestProcess.MakeRequest<TheMovieDbRootResult, TheMovieDbResult>(searchRequest);

					TheMovieDbResult matchedMovie = searchResponse.Results.FirstOrDefault();

					if (matchedMovie != null)
					{
						string title = matchedMovie.Title;

						Console.WriteLine("{0} - {1}", title, matchedMovie.ReleaseDate);

						string path = TheMovieDbResult.CreatePosterHyperlink(matchedMovie.PosterPath);
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Movie not found - {0}.", videoFile.FileInfo.Name);
						Console.ForegroundColor = ConsoleColor.White;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.Read();
				}
			}
		}
	}
}
