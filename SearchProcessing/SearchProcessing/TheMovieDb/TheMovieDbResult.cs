using SearchProcessing.Constracts;
using System.Runtime.Serialization;

namespace SearchProcessing.TheMovieDb
{
	[DataContract]
	public class TheMovieDbResult : IResult
	{
		#region Properties

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

		#endregion Properties

		#region Methods

		/// <summary>
		/// Creates the hyperlink of a poster image.
		/// </summary>
		/// <param name="posterPath">The PosterPath that is returned with the query result.</param>
		/// <returns>The complete hyperlink.</returns>
		public static string CreatePosterHyperlink(string posterPath)
		{
			return "http://image.tmdb.org/t/p/w500" + posterPath;
		}

		#endregion Methods
	}
}
