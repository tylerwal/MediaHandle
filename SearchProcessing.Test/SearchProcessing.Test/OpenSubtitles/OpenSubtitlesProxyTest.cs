using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CookComputing.XmlRpc;
using MediaHandleUtilities.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles;
using SearchProcessing.OpenSubtitles.Domain;
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
		
		#endregion Fields

		[TestInitialize]
		public void TestInitialize()
		{
			ConfigurationSettings.Initialize();

			_username = ConfigurationSettings.OpenSubtitles.Username;
			_password = ConfigurationSettings.OpenSubtitles.Password;
			_language = ConfigurationSettings.OpenSubtitles.Language;
			_userAgent = ConfigurationSettings.OpenSubtitles.UserAgent;

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

		#region LogIn Tests

		[TestMethod]
		public void LogInStatusPassTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");
			
			Assert.AreEqual(ResponseStatusLookupId.Ok, logInResponse.GetResponseStatus(), "The LogIn response status was not passing.");

			// still should log out to complete connection
			_proxy.LogOut(logInResponse.Token);
		}

		[TestMethod]
		public void LogInStatusFailTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, "wrongPassword", _language, _userAgent);

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");

			Assert.AreEqual(ResponseStatusLookupId.Unauthorized, logInResponse.GetResponseStatus(), "The LogIn response status was Ok when it should have been Unauthorized.");
		}

		[TestMethod]
		public void LogInUserNameFailTest()
		{
			LogInResponse logInResponse = _proxy.LogIn("wrongUsername", _password, _language, _userAgent);

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");

			Assert.AreEqual(ResponseStatusLookupId.Unauthorized, logInResponse.GetResponseStatus(), "The LogIn response status was Ok when it should have been Unauthorized.");
		}

		[TestMethod]
		public void LogInUserAgentFailTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, "wrongUsername");

			Assert.IsNotNull(logInResponse.Status, "The LogIn response status was null.");

			Assert.AreEqual(ResponseStatusLookupId.UnknownUserAgent, logInResponse.GetResponseStatus(), "The LogIn response status was Ok when it should have been Unknown UserAgent.");
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

		#endregion LogIn Tests
		
		[TestMethod]
		public void LogOutTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);
			
			BasicResponse basicResponse = _proxy.LogOut(logInResponse.Token);

			Assert.AreEqual(ResponseStatusLookupId.Ok, basicResponse.GetResponseStatus(), "The LogOut attempt did not result in an Ok status.");

			BasicResponse afterLogOutResponse = _proxy.SessionCheck(logInResponse.Token);

			Assert.AreEqual(ResponseStatusLookupId.NoSession, afterLogOutResponse.GetResponseStatus(), "The account is still logged in after a LogOut attempt.");
		}

		#region Search By Hash tests

		/// <summary>
		/// Tests the SearchByHash interface method.
		/// </summary>
		[TestMethod]
		public void SearchByHashTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);

			string hash = "073f8fc5d170434e";
			hash = "000";

			SearchByHashResponse searchResponse = _proxy.SearchByHash(logInResponse.Token, new string[]
			{
				hash
			});

			// foreach (XmlRpcStruct SearchResult in ((object[])ResponseStruct["data"]))

			IEnumerable<DictionaryEntry> dictionaryEnties = searchResponse.MediaData
															.OfType<DictionaryEntry>()
															.Where(i => i.Value.GetType() == typeof(XmlRpcStruct));

			int countOfEverything = dictionaryEnties.Count();
			int countOfStructs = dictionaryEnties.Count(i => i.Value.GetType() == typeof(XmlRpcStruct));

			foreach (DictionaryEntry test2 in searchResponse.MediaData)
			{
				var test3 = test2.Value;
				Type valueType = test3.GetType();
				Type test4 = test2.GetType();
			}

			var test = searchResponse.MediaData[hash];

			//var type = test.GetType();

			BasicResponse basicResponse = _proxy.LogOut(logInResponse.Token);
		}

		/// <summary>
		/// Tests the SearchByHash interface method when a known hash should only produce a single movie response.
		/// </summary>
		[TestMethod]
		public void SearchByHashSingleResponseTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);

			const string hash = "073f8fc5d170434e";

			SearchByHashResponse searchResponse = _proxy.SearchByHash(logInResponse.Token, new string[]
			{
				hash
			});

			IEnumerable<XmlRpcStruct> dictionaryEnties = searchResponse.MediaData
				.OfType<DictionaryEntry>()
				.Select(i => i.Value)
				.OfType<XmlRpcStruct>();

			int countOfEverything = dictionaryEnties.Count();

			BasicResponse basicResponse = _proxy.LogOut(logInResponse.Token);
		}

		#endregion Search By Hash tests
	}
}
