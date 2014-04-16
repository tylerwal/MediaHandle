using System;
using System.Net;
using System.Runtime.Serialization.Json;

namespace JsonProcessing
{
	public static class JsonRequest
	{
		#region Fields

		private const string _theMovieDbOrgUrl = "https://api.themoviedb.org";
		private const string _theMovieDbApiKey = "f52f11edacfe8bbf8b9978ddbaf76526";
		private const string _queryParameter = "&query=";

		#endregion Fields

		#region Methods

		public static string CreateRequest(string queryString)
		{
			string urlRequest = _theMovieDbOrgUrl +
							"/3/search/movie" +
							"?api_key=" + _theMovieDbApiKey +
							_queryParameter + queryString;

			return urlRequest;
		}

		public static MovieSearchAggregate MakeRequest(string requestUrl)
		{
			try
			{
				HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response != null && response.StatusCode != HttpStatusCode.OK)
					{
						throw new Exception(String.Format("Server Error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
					}

					DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(MovieSearchAggregate));
					
					object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
					
					MovieSearchAggregate jsonResponse = objResponse as MovieSearchAggregate;

					return jsonResponse;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}

		#endregion Methods

		
	}
}