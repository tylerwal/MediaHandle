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

			Assert.AreEqual(ResponseStatusLookupId.NoSession, response.Status, "The session was not ended correctly.");
		}
	}
}
