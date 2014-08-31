using CookComputing.XmlRpc;

using MediaHandleUtilities.Configuration;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles;
using SearchProcessing.OpenSubtitles.Domain;

namespace SearchProcessing.Test.OpenSubtitles
{
	[TestClass]
	public class OpenSubtitlesProxyWrapperTest
	{
		[TestMethod]
		public void LogInDisposeTest()
		{
			OpenSubtitlesProxyWrapper wrapper = new OpenSubtitlesProxyWrapper();

			ResponseStatusLookupId status = wrapper.GetSessionStatus();

			Assert.AreEqual(ResponseStatusLookupId.Ok, status, "The session's status was not in an Ok state.");

			wrapper.Dispose();

			status = wrapper.GetSessionStatus();

			Assert.AreEqual(ResponseStatusLookupId.NoSession, status, "The session was not ended correctly.");
		}

		[TestMethod]
		public void LogInLogOffTest()
		{
			OpenSubtitlesProxyWrapper wrapper = new OpenSubtitlesProxyWrapper();

			ResponseStatusLookupId status = wrapper.GetSessionStatus();

			Assert.AreEqual(ResponseStatusLookupId.Ok, status, "The session's status was not in an Ok state.");

			wrapper.LogOut();

			status = wrapper.GetSessionStatus();

			Assert.AreEqual(ResponseStatusLookupId.NoSession, status, "The session was not ended correctly.");
		}

		[TestMethod]
		public void UsingTest()
		{
			string token;

			using (OpenSubtitlesProxyWrapper wrapper = new OpenSubtitlesProxyWrapper())
			{
				ResponseStatusLookupId status = wrapper.GetSessionStatus();

				token = wrapper.Token;

				Assert.AreEqual(ResponseStatusLookupId.Ok, status, "The session's status was not in an Ok state.");
			}

			BasicResponse response = OpenSubtitlesProxyWrapper.SessionCheck(token);

			Assert.AreEqual(ResponseStatusLookupId.NoSession, response.GetResponseStatus(), "The session was not ended correctly.");
		}

		/// <summary>
		/// Tests the Token property.
		/// </summary>
		[TestMethod]
		public void TokenTest()
		{
			using (OpenSubtitlesProxyWrapper wrapper = new OpenSubtitlesProxyWrapper())
			{
				Assert.IsNotNull(wrapper.Token, "The Token should not be null.");

				Assert.AreNotEqual(string.Empty, wrapper.Token, "The Token should not be empty.");
			}
		}

		/// <summary>
		/// Tests the Token property when the LogInResponse is null.
		/// </summary>
		[TestMethod]
		public void TokenLogInResponseNullTest()
		{
			using (OpenSubtitlesProxyWrapper wrapper = new OpenSubtitlesProxyWrapper())
			{
				// revert to this later so that correctly done Dispose (log out) can occure
				LogInResponse savedLogInResponse = wrapper.LogInResponse;

				wrapper.LogInResponse = null;

				Assert.IsNull(wrapper.Token, "The Token should be null.");

				wrapper.LogInResponse = savedLogInResponse;
			}
		}
	}
}
