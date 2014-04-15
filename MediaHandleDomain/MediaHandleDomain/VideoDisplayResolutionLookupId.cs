using MediaHandleUtilities;

namespace MediaHandleDomain
{
	public enum VideoDisplayResolutionLookupId
	{
		[StringValue("None")]
		None = 0,

		[StringValue("480p")]
		FourEightyPixels = 1,

		[StringValue("720p")]
		SevenTwentyPixels = 2,

		[StringValue("1080p")]
		TenEightyPixels = 3,
	}
}