using CookComputing.XmlRpc;
using MediaHandleUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles;
using SearchProcessing.OpenSubtitles.Domain;
using SearchProcessing.Utilities;
using System.Text.RegularExpressions;

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

		private IOpenSubtitlesProxy _proxy;

		private readonly string _statusOk = EnumUtilities.GetStringValue(ResponseStatusLookupId.Ok);
		private readonly string _statusUnauthorized = EnumUtilities.GetStringValue(ResponseStatusLookupId.Unauthorized);
		private readonly string _statusUnknownUserAgent = EnumUtilities.GetStringValue(ResponseStatusLookupId.UnknownUserAgent);

		#endregion Fields

		[TestInitialize]
		public void TestInitialize()
		{
			Configuration.Initialize();

			_username = Configuration.OpenSubtitles.Username;
			_password = Configuration.OpenSubtitles.Password;
			_language = Configuration.OpenSubtitles.Language;
			_userAgent = Configuration.OpenSubtitles.UserAgent;

			_proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();
		}

		/// <summary>
		/// Test to make sure the XMLRPC connection is working with the most basic method on OpenSubtitles: ServerInfo()
		/// 
		/// ServerInfo() does not require a login;
		/// </summary>
		[TestMethod]
		public void ServerInfoTest()
		{
			ServerInfo serviceInfo = _proxy.ServiceInfo();

			Assert.AreEqual("http://api.opensubtitles.org/xml-rpc", serviceInfo.XmlRpcUrl, "The ServerInfo() method did not returned the expected xmlrpc url.");

			Assert.AreEqual("http://www.opensubtitles.org", serviceInfo.WebsiteUrl, "The ServerInfo() method did not returned the expected website url.");
		}

		[TestMethod]
		public void LogInStatusPassTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");
			
			Assert.AreEqual(_statusOk, logInResponse.Status, "The LogIn response status was not passing.");
		}

		[TestMethod]
		public void LogInStatusFailTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, "wrongPassword", _language, _userAgent);

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");

			Assert.AreEqual(_statusUnauthorized, logInResponse.Status, "The LogIn response status was Ok when it should have been Unauthorized.");
		}

		[TestMethod]
		public void LogInUserNameFailTest()
		{
			LogInResponse logInResponse = _proxy.LogIn("wrongUsername", _password, _language, _userAgent);

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");

			Assert.AreEqual(_statusUnauthorized, logInResponse.Status, "The LogIn response status was Ok when it should have been Unauthorized.");
		}

		[TestMethod]
		public void LogInUserAgentFailTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, "wrongUsername");

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");

			Assert.AreEqual(_statusUnknownUserAgent, logInResponse.Status, "The LogIn response status was Ok when it should have been Unknown UserAgent.");
		}
		
		[TestMethod]
		public void LogInTokenTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);

			Assert.IsNotNull(logInResponse.Token, "The LogIn response token was null.");

			Regex tokenRegex = new Regex(@"[0-9a-z]+", RegexOptions.Compiled);
			bool isMatch = tokenRegex.Match(logInResponse.Token).Success;
			Assert.IsTrue(isMatch, "The LogIn response token was not in the expected form.");
		}

		[TestMethod]
		public void LogOutTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);
			
			BasicResponse basicResponse = _proxy.LogOut(logInResponse.Token);

			Assert.AreEqual(_statusOk, basicResponse.Status, "The LogOut attempt did not result in an Ok status.");
		}
	}
}
