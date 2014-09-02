using MediaHandleUtilities.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.TheMovieDb;

namespace SearchProcessing.Test
{
	/// <summary>
	/// Tests the <see cref="RequestUtilities"/> class.
	/// </summary>
	[TestClass]
	public class RequestUtilitiesTest
	{
		/// <summary>
		/// Tests the MakeRequest method.
		/// </summary>
		[TestMethod]
		public void MakeRequestTest()
		{
			string testValue = "testValue";

			string expectedValue = ConfigurationSettings.TheMovieDb.Url +
				"/3/search/movie" +
				"?api_key=" +
				ConfigurationSettings.TheMovieDb.ApiKey +
				QueryConstants._queryParameter +
				testValue;

			string actualValue = TheMovieDbRequest.CreateSearchQuery(testValue);

			Assert.AreEqual(expectedValue, actualValue, "The two URLs did not match.");
		}
	}
}
