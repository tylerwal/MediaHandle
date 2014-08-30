using MediaHandleUtilities;

namespace SearchProcessing.OpenSubtitles.Domain
{
	/// <summary>
	/// Facade for a <see cref="SearchByHashResponse"/> object so that no dictionary keys are required; 
	/// just pass the SearchByHashResponse in and this takes care of the rest.
	/// </summary>
	public class SearchByHashResponseWrapper
	{
		#region Properties

		public string Hash
		{
			get; 
			private set;
		}

		public string ImdbId
		{
			get;
			private set; 
		}

		public string Name
		{
			get;
			private set;
		}

		public int? Year
		{
			get;
			private set;
		}

		public int KindLookupId
		{
			get;
			private set;
		}

		public int? Season
		{
			get;
			private set;
		}

		public int? Episode
		{
			get;
			private set;
		}

		public int? SubtitlesCount
		{
			get;
			private set;
		}

		#endregion Properties

		public SearchByHashResponseWrapper(SearchByHashResponse basicResponse)
		{
			Hash = basicResponse.GetMediaDataField(SearchByHashResponse._movieHash);

			ImdbId = basicResponse.GetMediaDataField(SearchByHashResponse._movieImdbId);

			Year = ParseNumberField(basicResponse, SearchByHashResponse._movieYear);

			Name = basicResponse.GetMediaDataField(SearchByHashResponse._movieName);
			
			KindLookupId = (int)EnumUtilities.GetMatchingEnum<OpenSubtitlesKind>(
				basicResponse.GetMediaDataField(
					SearchByHashResponse._movieKind
				)
			);

			Season = ParseNumberField(basicResponse, SearchByHashResponse._seriesSeason);

			Episode = ParseNumberField(basicResponse, SearchByHashResponse._seriesEpisode);

			SubtitlesCount = ParseNumberField(basicResponse, SearchByHashResponse._subCount);
		}

		#region Helper Methods

		/// <summary>
		/// Sets the value of a nullable int based on the string value of the dictionary.
		/// </summary>
		/// <param name="response">The <see cref="SearchByHashResponse"/> object.</param>
		/// <param name="key">The dictionary key.</param>
		/// <returns>The int value or null based on if the int parse was successful.</returns>
		private int? ParseNumberField(SearchByHashResponse response, string key)
		{
			int possibleNumber;
			string returnedMovieYear = response.GetMediaDataField(key);
			bool intParseSuccessful = int.TryParse(returnedMovieYear, out possibleNumber);

			if (intParseSuccessful)
			{
				return possibleNumber;
			}
			return null;
		}

		#endregion Helper Methods

	}
}