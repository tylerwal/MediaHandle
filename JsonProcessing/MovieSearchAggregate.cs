using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JsonProcessing
{
	[DataContract]
	public class MovieSearchAggregate
	{
		[DataMember(Name = "page")]
		public int Page { get; set; }

		[DataMember(Name = "results")]
		public List<MovieSearchResult> Results { get; set; }

		[DataMember(Name = "total_pages")]
		public int TotalPages { get; set; }

		[DataMember(Name = "total_results")]
		public int TotalResults { get; set; } 
	}
}