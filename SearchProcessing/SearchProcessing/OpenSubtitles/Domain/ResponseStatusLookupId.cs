using MediaHandleUtilities;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public enum ResponseStatusLookupId
	{
		[StringValue("None")]
		None = 0,

		[StringValue("200 OK")]
		Ok = 1,

		[StringValue("401 Unauthorized")]
		Unauthorized = 2,

		[StringValue("414 Unknown User Agent")]
		UnknownUserAgent = 3
	}
}