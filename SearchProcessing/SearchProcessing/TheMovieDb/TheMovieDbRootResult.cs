using SearchProcessing.Constracts;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SearchProcessing.TheMovieDb
{
	[DataContract]
	public class TheMovieDbRootResult : IRootObject<TheMovieDbResult>
	{
		[DataMember(Name = "page")]
		public int Page { get; set; }

		[DataMember(Name = "results")]
		public List<TheMovieDbResult> Results { get; set; }

		[DataMember(Name = "total_pages")]
		public int TotalPages { get; set; }

		[DataMember(Name = "total_results")]
		public int TotalResults { get; set; }
	}
}