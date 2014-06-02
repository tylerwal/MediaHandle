using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchProcessing.Test.OpenSubtitles
{
	[TestClass]
	public class OpenSubtitlesConfigurationTest
	{
		[TestInitialize]
		public void TestInitialize()
		{
			Utilities.Configuration.Initialize();
		}

		[TestMethod]
		public void UsernameTest()
		{
			string username = Utilities.Configuration.OpenSubtitles.Username;

			Assert.IsNotNull(username, "The Username was not found in the app.config file.");
			Assert.AreNotEqual(username, string.Empty, "The Username was empty in the app.config file.");
		}

		[TestMethod]
		public void PasswordTest()
		{
			string password = Utilities.Configuration.OpenSubtitles.Password;

			Assert.IsNotNull(password, "The Password was not found in the app.config file.");
			Assert.AreNotEqual(password, string.Empty, "The Password was empty in the app.config file.");
		}

		[TestMethod]
		public void LanguageTest()
		{
			string language = Utilities.Configuration.OpenSubtitles.Language;

			Assert.IsNotNull(language, "The Language was not found in the app.config file.");
			Assert.AreNotEqual(language, string.Empty, "The Language was empty in the app.config file.");
		}

		[TestMethod]
		public void UserAgentTest()
		{
			string userAgent = Utilities.Configuration.OpenSubtitles.UserAgent;

			Assert.IsNotNull(userAgent, "The UserAgent was not found in the app.config file.");
			Assert.AreNotEqual(userAgent, string.Empty, "The UserAgent was empty in the app.config file.");
		}
	}
}
