using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace MediaHandleConsole
{
	class Program
	{
		#region Fields

		private const string _theMovieDbOrgUrl = "https://api.themoviedb.org";
		private const string _theMovieDbApiKey = "f52f11edacfe8bbf8b9978ddbaf76526";
		private const string _queryParameter = "&query=";

		private List<string> _mediaFileExtensions = new List<string>
					{
						".mkv",
						".avi",
						".mpg",
						".wmv",
						".mp4"
					};	

		#endregion Fields

		static void Main(string[] args)
		{
			Program prog = new Program();

			//prog.FindMovies();

			prog.TestJson();
		}

		private void FindMovies()
		{
			DirectoryInfo moviesDirectoryInfo = new DirectoryInfo(@"\\SERVER\Downloads\Movies\");

			var fileSystemInfos = moviesDirectoryInfo.EnumerateFileSystemInfos("*", SearchOption.AllDirectories);

			//var files = moviesDirectoryInfo.EnumerateFiles("*", SearchOption.AllDirectories);

			var fileList = moviesDirectoryInfo.GetFiles("*", SearchOption.AllDirectories).ToList();

			var matches =
				from fileInfo in fileList
				from mediaFileExtension in _mediaFileExtensions
				where fileInfo.Name.Contains(mediaFileExtension)
				select fileInfo;

			var m = matches.Count();

			var betterMatches = fileList
				.Where(f =>
				{
					bool isMatch = false;

					foreach (string mediaFileExtension in _mediaFileExtensions)
					{
						if (f.Name.Contains(mediaFileExtension))
						{
							isMatch = true;
						}
					}

					return isMatch;
				});

			var bm = betterMatches.Count();

			var optoMatches = fileList.Where(f => _mediaFileExtensions.Any(f.Name.Contains));

			var om = optoMatches.Count();

			var eventBetter = fileList.Where(f => _mediaFileExtensions.Any(f.Extension.Equals));

			var eb = eventBetter.Count();

			var mediaSystemFiles = fileSystemInfos.Where(i => i.Name.Contains(_mediaFileExtensions.First()));
			
			//var mediaFiles = files.Where(i => i.Name.Contains(_mediaFileExtensions.First()));
		}

		private void TestJson()
		{
			try
			{
				string searchRequest = CreateRequest("The Matrix Revisited");

				RootObject searchResponse = MakeRequest(searchRequest);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.Read();
			}
		}

		#region Helper Methods
		
		private static string CreateRequest(string queryString)
		{
			string urlRequest = _theMovieDbOrgUrl +
							"/3/search/movie" +
							"?api_key=" + _theMovieDbApiKey +
							_queryParameter + queryString;

			return urlRequest;
		}

		private static RootObject MakeRequest(string requestUrl)
		{
			try
			{
				HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
					{
						throw new Exception(String.Format("Server Error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
					}

					DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RootObject));

					//Stream text = response.GetResponseStream();

					//var sr = new StreamReader(text);

					//var mystr = sr.ReadToEnd();

					object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());



					RootObject jsonResponse = objResponse as RootObject;

					return jsonResponse;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}

		#endregion Helper Methods
	}
	
	[DataContract]
	public class Result
	{
		[DataMember(Name = "adult")]
		public bool Adult { get; set; }

		[DataMember(Name = "backdrop_path")]
		public string BackdropPath { get; set; }

		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "original_title")]
		public string OriginalTitle { get; set; }

		[DataMember(Name = "release_date")]
		public string ReleaseDate { get; set; }

		[DataMember(Name = "poster_path")]
		public string PosterPath { get; set; }

		[DataMember(Name = "popularity")]
		public double Popularity { get; set; }

		[DataMember(Name = "title")]
		public string Title { get; set; }

		[DataMember(Name = "vote_average")]
		public double VoteAverage { get; set; }

		[DataMember(Name = "vote_count")]
		public int VoteCount { get; set; }
	}

	[DataContract]
	public class RootObject
	{
		[DataMember(Name = "page")]
		public int Page { get; set; }

		[DataMember(Name = "results")]
		public List<Result> Results { get; set; }

		[DataMember(Name = "total_pages")]
		public int TotalPages { get; set; }

		[DataMember(Name = "total_results")]
		public int TotalResults { get; set; }
	}
}
