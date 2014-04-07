using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Net;

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
				string searchRequest = CreateRequest("Matrix");

				Response searchResponse = MakeRequest(searchRequest);
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

		private static Response MakeRequest(string requestUrl)
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

					DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));

					object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
					
					Response jsonResponse = objResponse as Response;

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
	public class Response
	{
		[DataMember(Name = "title")]
		public string OriginalTitle
		{
			get;
			set;
		}
	}
}
