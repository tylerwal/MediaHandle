using SearchProcessing.Constracts;

namespace SearchProcessing.TheMovieDb
{
	public class TheMovieDbRequest : IRequest
	{
		public string CreateSearchQuery(string queryString)
		{
			string urlRequest = QueryConstants._theMovieDbOrgUrl +
							"/3/search/movie" +
							"?api_key=" + QueryConstants._theMovieDbApiKey +
							QueryConstants._queryParameter + queryString;

			return urlRequest;
		}
	}
}