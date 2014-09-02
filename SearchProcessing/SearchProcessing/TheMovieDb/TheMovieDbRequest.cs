using MediaHandleUtilities.Configuration;
using SearchProcessing.Constracts;
using System.Text;

namespace SearchProcessing.TheMovieDb
{
	public class TheMovieDbRequest : IRequest
	{
		public static string CreateSearchQuery(string queryString)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(ConfigurationSettings.TheMovieDb.Url);
			sb.Append("/3/search/movie");
			sb.Append("?api_key=");
			sb.Append(ConfigurationSettings.TheMovieDb.ApiKey);
			sb.Append(QueryConstants._queryParameter);
			sb.Append(queryString);

			return sb.ToString();
		}
	}
}