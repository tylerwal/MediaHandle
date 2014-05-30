using System.Configuration;
using System.Text.RegularExpressions;

using CookComputing.XmlRpc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles;
using SearchProcessing.OpenSubtitles.Domain;

namespace SearchProcessing.Test.OpenSubtitles
{
	[TestClass]
	public class OpenSubtitlesProxyTest
	{
		#region Fields

		private string _username;
		private string _password;
		private string _language;
		private string _userAgent;

		private const string STATUS_OK = "200 OK";
		private const string STATUS_UNAUTHORIZED = "401 Unauthorized";

		#endregion Fields

		[TestInitialize]
		public void TestInitialize()
		{
			_username = ConfigurationManager.AppSettings.Get("Username");
			_password = ConfigurationManager.AppSettings.Get("Password");
			_language = ConfigurationManager.AppSettings.Get("Language");
			_userAgent = ConfigurationManager.AppSettings.Get("UserAgent");
		}

		/// <summary>
		/// Test to make sure the XMLRPC connection is working with the most basic method on OpenSubtitles: ServerInfo()
		/// 
		/// ServerInfo() does not require a login;
		/// </summary>
		[TestMethod]
		public void ServerInfoTest()
		{
			IOpenSubtitlesProxy proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();

			ServerInfo serviceInfo = proxy.ServiceInfo();

			Assert.AreEqual("http://api.opensubtitles.org/xml-rpc", serviceInfo.XmlRpcUrl, "The ServerInfo() method did not returned the expected xmlrpc url.");

			Assert.AreEqual("http://www.opensubtitles.org", serviceInfo.WebsiteUrl, "The ServerInfo() method did not returned the expected website url.");
		}

		[TestMethod]
		public void LogInStatusPassTest()
		{
			IOpenSubtitlesProxy proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();
			
			LogInResponse logInResponse = proxy.LogIn(_username, _password, _language, _userAgent);

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");
			
			Assert.AreEqual(STATUS_OK, logInResponse.Status, "The LogIn response status was not passing.");
		}

		[TestMethod]
		public void LogInStatusFailTest()
		{
			IOpenSubtitlesProxy proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();

			LogInResponse logInResponse = proxy.LogIn(_username, "wrongPassword", _language, _userAgent);

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");

			Assert.AreEqual(STATUS_UNAUTHORIZED, logInResponse.Status, "The LogIn response status was not passing.");
		}

		[TestMethod]
		public void LogInTokenTest()
		{
			IOpenSubtitlesProxy proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();

			LogInResponse logInResponse = proxy.LogIn(_username, _password, _language, _userAgent);

			Assert.IsNotNull(logInResponse.Token, "The LogIn response token was null.");

			Regex tokenRegex = new Regex(@"[0-9a-z]+", RegexOptions.Compiled);
			bool isMatch = tokenRegex.Match(logInResponse.Token).Success;
			Assert.IsTrue(isMatch, "The LogIn response token was not in the expected form.");
		}
	}
}
