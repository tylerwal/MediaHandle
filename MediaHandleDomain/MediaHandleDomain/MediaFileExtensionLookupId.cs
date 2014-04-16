using MediaHandleUtilities;

namespace MediaHandleDomain
{
	[StringEnumClass]
	public enum MediaFileExtensionLookupId
	{
		[StringValue("None")]
		None = 0,

		[StringValue(".mkv")]
		Mkv = 1,

		[StringValue(".avi")]
		Avi = 2,

		[StringValue(".mpg")]
		Mpg = 3,

		[StringValue(".wmv")]
		Wmv = 4,

		[StringValue(".mp4")]
		Mp4 = 5
	}
}