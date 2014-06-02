using SearchProcessing.OpenSubtitles;
using System.Configuration;

namespace SearchProcessing.Utilities
{
	public static class Configuration
	{
		private static OpenSubtitlesConfiguration _openSubtitlesConfiguration;

		public static OpenSubtitlesConfiguration OpenSubtitles
		{
			get { return _openSubtitlesConfiguration;  }
			internal set { _openSubtitlesConfiguration = value;  }
		}

		public static void Initialize()
		{
			_openSubtitlesConfiguration = ConfigurationManager.GetSection("OpenSubtitles") as OpenSubtitlesConfiguration;
		}
	}
}