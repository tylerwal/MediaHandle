using System.Configuration;

namespace MediaHandleUtilities.Configuration
{
	public static class ConfigurationSettings
	{
		public static OpenSubtitlesConfiguration OpenSubtitles { get; private set; }

		public static void Initialize()
		{
			OpenSubtitles = ConfigurationManager.GetSection("OpenSubtitles") as OpenSubtitlesConfiguration;
		}
	}
}