using CookComputing.XmlRpc;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class MovieResponse
	{
		[XmlRpcMember("MovieHash")]
		public string Hash { get; set; }

		[XmlRpcMember("MovieImdbId")]
		public string ImdbId { get; set; }

		[XmlRpcMember("MovieName")]
		public string Name { get; set; }

		[XmlRpcMember("MovieYear")]
		public string Year { get; set; }

		[XmlRpcMember("Kind")]
		public string Kind { get; set; }
	}
}