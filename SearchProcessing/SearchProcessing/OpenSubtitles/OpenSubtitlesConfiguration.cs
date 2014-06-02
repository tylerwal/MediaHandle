using SearchProcessing.Annotations;
using System.Configuration;

namespace SearchProcessing.OpenSubtitles
{
	[UsedImplicitly]
	public class OpenSubtitlesConfiguration : ConfigurationSection
	{
		[ConfigurationProperty("Username")]
		public string Username
		{
			get { return (string)this["Username"]; }
			set { this["Username"] = value; }
		}

		[ConfigurationProperty("Password")]
		public string Password
		{
			get { return (string)this["Password"]; }
			set { this["Password"] = value; }
		}

		[ConfigurationProperty("Language")]
		public string Language
		{
			get { return (string)this["Language"]; }
			set { this["Language"] = value; }
		}

		[ConfigurationProperty("UserAgent")]
		public string UserAgent
		{
			get { return (string)this["UserAgent"]; }
			set { this["UserAgent"] = value; }
		}
	}
}
