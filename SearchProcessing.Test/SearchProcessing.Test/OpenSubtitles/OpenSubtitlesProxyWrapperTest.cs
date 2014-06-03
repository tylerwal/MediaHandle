using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles;
using SearchProcessing.OpenSubtitles.Domain;

namespace SearchProcessing.Test.OpenSubtitles
{
	[TestClass]
	public class OpenSubtitlesProxyWrapperTest
	{
		[TestMethod]
		public void LogInLogOutTest()
		{
			OpenSubtitlesProxyWrapper wrapper = new OpenSubtitlesProxyWrapper();

			ResponseStatusLookupId status = wrapper.GetSessionStatus();

			Assert.AreEqual(ResponseStatusLookupId.Ok, status, "The session's status was not in an Ok state.");

			wrapper.Dispose();

			status = wrapper.GetSessionStatus();

			Assert.AreEqual(ResponseStatusLookupId.NoSession, status, "The session was not ended correctly.");
		}
	}
}
