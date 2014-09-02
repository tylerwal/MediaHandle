using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaHandleUtilities.Configuration;

namespace MediaHandleUtilities.Test.Configuration
{
	[TestClass]
	public class OpenSubtitlesConfigurationTest
	{
		[TestMethod]
		public void UrlTest()
		{
			string url = ConfigurationSettings.OpenSubtitles.Url;

			Assert.IsNotNull(url, "The Username was not found in the app.config file.");
			Assert.AreNotEqual(url, string.Empty, "The Username was empty in the app.config file.");
		}

		[TestMethod]
		public void UsernameTest()
		{
			string username = ConfigurationSettings.OpenSubtitles.Username;

			Assert.IsNotNull(username, "The Username was not found in the app.config file.");
			Assert.AreNotEqual(username, string.Empty, "The Username was empty in the app.config file.");
		}

		[TestMethod]
		public void PasswordTest()
		{
			string password = ConfigurationSettings.OpenSubtitles.Password;

			Assert.IsNotNull(password, "The Password was not found in the app.config file.");
			Assert.AreNotEqual(password, string.Empty, "The Password was empty in the app.config file.");
		}

		[TestMethod]
		public void LanguageTest()
		{
			string language = ConfigurationSettings.OpenSubtitles.Language;

			Assert.IsNotNull(language, "The Language was not found in the app.config file.");
			Assert.AreNotEqual(language, string.Empty, "The Language was empty in the app.config file.");
		}

		[TestMethod]
		public void UserAgentTest()
		{
			string userAgent = ConfigurationSettings.OpenSubtitles.UserAgent;

			Assert.IsNotNull(userAgent, "The UserAgent was not found in the app.config file.");
			Assert.AreNotEqual(userAgent, string.Empty, "The UserAgent was empty in the app.config file.");
		}
	}
}
