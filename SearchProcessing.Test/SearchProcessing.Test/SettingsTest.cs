using System.Configuration;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchProcessing.Test
{
	[TestClass]
	public class SettingsTest
	{
		[TestMethod]
		public void UsernameTest()
		{
			string username = ConfigurationManager.AppSettings.Get("Username");
			Assert.IsNotNull(username, "The Username was not found in the app.config file.");
			Assert.AreNotEqual(username, string.Empty, "The Username was empty in the app.config file.");
		}

		[TestMethod]
		public void PasswordTest()
		{
			string password = ConfigurationManager.AppSettings.Get("Password");
			Assert.IsNotNull(password, "The Password was not found in the app.config file.");
			Assert.AreNotEqual(password, string.Empty, "The Password was empty in the app.config file.");
		}

		[TestMethod]
		public void LanguageTest()
		{
			string language = ConfigurationManager.AppSettings.Get("Language");
			Assert.IsNotNull(language, "The Language was not found in the app.config file.");
			Assert.AreNotEqual(language, string.Empty, "The Language was empty in the app.config file.");
		}

		[TestMethod]
		public void UserAgentTest()
		{
			string userAgent = ConfigurationManager.AppSettings.Get("UserAgent");
			Assert.IsNotNull(userAgent, "The UserAgent was not found in the app.config file.");
			Assert.AreNotEqual(userAgent, string.Empty, "The UserAgent was empty in the app.config file.");
		}

		[TestMethod]
		public void FailTest()
		{
			string result = ConfigurationManager.AppSettings.Get("NotReal");
			Assert.IsNull(result, "The app.config file is not being read correctly.");
		}
	}
}
