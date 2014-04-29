using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using FileProcessing;

using MediaHandleDomain;

using MediaHandleUtilities;

namespace MediaHandleConsole
{
	class Program
	{
		#region Fields

		private const string _theMovieDbOrgUrl = "https://api.themoviedb.org";
		private const string _theMovieDbApiKey = "f52f11edacfe8bbf8b9978ddbaf76526";
		private const string _queryParameter = "&query=";
		
		List<string> _movieFileExtensions = EnumUtilities.GetStringValuesExceptNone(typeof(MediaFileExtensionLookupId)); 
		
		List<string> _videoDisplayResolutions = EnumUtilities.GetStringValuesExceptNone(typeof(VideoDisplayResolutionLookupId));

		#endregion Fields

		static void Main(string[] args)
		{
			FileProcess fileProcess = new FileProcess(@"\\SERVER\Downloads\Movies\");

			List<FileInfo> allMovieFiles = fileProcess.GetMovieFiles();

			IEnumerable<FileInfo> sampleFiles = FileProcess.GetProbableSampleFiles(allMovieFiles);

			IEnumerable<FileInfo> wantedMovieFiles = allMovieFiles.Except(sampleFiles);

			Program prog = new Program();
			
			prog.TestJson();
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
