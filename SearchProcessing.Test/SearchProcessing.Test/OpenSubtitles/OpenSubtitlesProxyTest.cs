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

		#region ServerInfo

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

		#endregion ServerInfo

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

		#region LogOut

		[TestMethod]
		public void LogOutTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);
			
			BasicResponse basicResponse = _proxy.LogOut(logInResponse.Token);

			Assert.AreEqual(ResponseStatusLookupId.Ok, basicResponse.GetResponseStatus(), "The LogOut attempt did not result in an Ok status.");

			BasicResponse afterLogOutResponse = _proxy.SessionCheck(logInResponse.Token);

			Assert.AreEqual(ResponseStatusLookupId.NoSession, afterLogOutResponse.GetResponseStatus(), "The account is still logged in after a LogOut attempt.");
		}

		#endregion LogOut

		#region SearchByHash tests

		/// <summary>
		/// Tests the SearchByHash interface method when a known hash should only produce a single movie response.
		/// </summary>
		[TestMethod]
		public void SearchByHashMovieMatchFoundTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);

			string hash = "1e8af369c4b2536d";

			// hard coded movie = Ender's Game
			// imdb @ http://www.imdb.com/title/tt1731141/
			// open subtitles @ http://www.imdb.com/title/tt1731141/

			SearchByHashResponse searchResponse = _proxy.SearchByHash(
				logInResponse.Token, 
				new string[] { hash }
			);
			
			object possibleMatch = searchResponse.MediaData[hash];
			Assert.IsInstanceOfType(possibleMatch, typeof(XmlRpcStruct), "This hash should have been a match; please check manually.");
			
			XmlRpcStruct match = possibleMatch as XmlRpcStruct;

			string returnedMovieHash = searchResponse.GetMediaDataField(SearchByHashResponse._movieHash);
			Assert.AreEqual(hash, returnedMovieHash, "The returned hash did not match the initial hash.");

			string returnedMovieImdbId = searchResponse.GetMediaDataField(SearchByHashResponse._movieImdbId);
			Assert.AreEqual("1731141", returnedMovieImdbId, "The returned IMDB Id did not match the expected value.");

			string returnedMovieYear = searchResponse.GetMediaDataField(SearchByHashResponse._movieYear);
			Assert.AreEqual("2013", returnedMovieYear, "The returned year did not match the expected value.");

			string returnedMovieName = searchResponse.GetMediaDataField(SearchByHashResponse._movieName);
			Assert.AreEqual("Ender's Game", returnedMovieName, "The returned movie name did not match the expected value.");

			string returnedMovieKind = searchResponse.GetMediaDataField(SearchByHashResponse._movieKind);
			Assert.AreEqual("movie", returnedMovieKind, "The returned movie kind did not match the expected value.");

			string returnedSeriesEpisode = searchResponse.GetMediaDataField(SearchByHashResponse._seriesEpisode);
			Assert.AreEqual("0", returnedSeriesEpisode, "The returned series episode was not 0.");

			string returnedSeriesSeason = searchResponse.GetMediaDataField(SearchByHashResponse._seriesSeason);
			Assert.AreEqual("0", returnedSeriesSeason, "The returned series season was not 0.");

			string returnedSeenCount = searchResponse.GetMediaDataField(SearchByHashResponse._seenCount);
			int possibleNumber;
			bool wasSuccesful = int.TryParse(returnedSeenCount, out possibleNumber);
			Assert.IsTrue(wasSuccesful, "The returned seen count was not a number.");

			string returnedSubCount = searchResponse.GetMediaDataField(SearchByHashResponse._subCount);
			wasSuccesful = int.TryParse(returnedSubCount, out possibleNumber);
			Assert.IsTrue(wasSuccesful, "The returned seen count was not a number.");
			
			BasicResponse basicResponse = _proxy.LogOut(logInResponse.Token);
		}

		/// <summary>
		/// Tests the SearchByHash interface method when a known hash should only produce a single tv episode response.
		/// </summary>
		[TestMethod]
		public void SearchByHashTvEpisodeMatchFoundTest()
		{
			LogInResponse logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);

			string hash = "1e8af369c4b2536d";

			// hard coded movie = Ender's Game
			// imdb @ http://www.imdb.com/title/tt1731141/
			// open subtitles @ http://www.imdb.com/title/tt1731141/

			SearchByHashResponse searchResponse = _proxy.SearchByHash(
				logInResponse.Token,
				new string[] { hash }
			);

			object possibleMatch = searchResponse.MediaData[hash];
			Assert.IsInstanceOfType(possibleMatch, typeof(XmlRpcStruct), "This hash should have been a match; please check manually.");

			XmlRpcStruct match = possibleMatch as XmlRpcStruct;

			string returnedMovieHash = searchResponse.GetMediaDataField(SearchByHashResponse._movieHash);
			Assert.AreEqual(hash, returnedMovieHash, "The returned hash did not match the initial hash.");

			string returnedMovieImdbId = searchResponse.GetMediaDataField(SearchByHashResponse._movieImdbId);
			Assert.AreEqual("1731141", returnedMovieImdbId, "The returned IMDB Id did not match the expected value.");

			string returnedMovieYear = searchResponse.GetMediaDataField(SearchByHashResponse._movieYear);
			Assert.AreEqual("2013", returnedMovieYear, "The returned year did not match the expected value.");

			string returnedMovieName = searchResponse.GetMediaDataField(SearchByHashResponse._movieName);
			Assert.AreEqual("Ender's Game", returnedMovieName, "The returned movie name did not match the expected value.");

			string returnedMovieKind = searchResponse.GetMediaDataField(SearchByHashResponse._movieKind);
			Assert.AreEqual("movie", returnedMovieKind, "The returned movie kind did not match the expected value.");

			string returnedSeriesEpisode = searchResponse.GetMediaDataField(SearchByHashResponse._seriesEpisode);
			Assert.AreEqual("0", returnedSeriesEpisode, "The returned series episode was not 0.");

			string returnedSeriesSeason = searchResponse.GetMediaDataField(SearchByHashResponse._seriesSeason);
			Assert.AreEqual("0", returnedSeriesSeason, "The returned series season was not 0.");

			string returnedSeenCount = searchResponse.GetMediaDataField(SearchByHashResponse._seenCount);
			int possibleNumber;
			bool wasSuccesful = int.TryParse(returnedSeenCount, out possibleNumber);
			Assert.IsTrue(wasSuccesful, "The returned seen count was not a number.");

			string returnedSubCount = searchResponse.GetMediaDataField(SearchByHashResponse._subCount);
			wasSuccesful = int.TryParse(returnedSubCount, out possibleNumber);
			Assert.IsTrue(wasSuccesful, "The returned seen count was not a number.");

			BasicResponse basicResponse = _proxy.LogOut(logInResponse.Token);
		}

		#endregion SearchByHash tests
	}
}
