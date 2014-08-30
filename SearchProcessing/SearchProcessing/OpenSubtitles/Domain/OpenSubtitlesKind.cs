using MediaHandleUtilities;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public enum OpenSubtitlesKind
	{
		[StringValue("None")]
		None = 0,

		[StringValue("movie")]
		Movie = 1,

		[StringValue("episode")]
		Episode = 2
	}
}