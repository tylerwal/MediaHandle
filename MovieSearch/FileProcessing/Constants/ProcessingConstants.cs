using MediaHandleDomain;
using MediaHandleUtilities;
using System.Collections.Generic;

namespace FileProcessing.Constants
{
	public static class ProcessingConstants
	{
		#region Fields

		private static readonly IEnumerable<string> _junkWordsToIgnore;

		private static readonly IEnumerable<string> _videoFileExtensionStrings;

		private static readonly IEnumerable<VideoDisplayResolutionLookupId> _videoDisplayResolutionEnums;

		#endregion Fields

		#region Constructor

		static ProcessingConstants()
		{
			_junkWordsToIgnore = new List<string>
			{
				"BluRay",
				"x264",
				"XVID",
				"AC3",
				"DVDScr",
				"UNRATED",
				"DvDrip",
				"EXTENDED"
			};

			_videoFileExtensionStrings = EnumUtilities.GetStringValuesExceptNone<MediaFileExtensionLookupId>();

			_videoDisplayResolutionEnums = EnumUtilities.GetEnumValueList<VideoDisplayResolutionLookupId>();
		}

		#endregion Constructor

		#region Properties

		public static IEnumerable<string> JunkWordsToIgnore
		{
			get
			{
				return _junkWordsToIgnore;
			}
		}

		public static IEnumerable<string> VideoFileExtensionStrings
		{
			get
			{
				return _videoFileExtensionStrings;
			}
		}

		public static IEnumerable<VideoDisplayResolutionLookupId> VideoDisplayResolutionEnums
		{
			get
			{
				return _videoDisplayResolutionEnums;
			}
		}

		#endregion Properties
	}
}