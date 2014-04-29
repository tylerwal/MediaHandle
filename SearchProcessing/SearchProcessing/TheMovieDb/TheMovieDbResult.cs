using SearchProcessing.Constracts;
using System.Runtime.Serialization;

namespace SearchProcessing.TheMovieDb
{
	[DataContract]
	public class TheMovieDbResult : IResult
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
}
