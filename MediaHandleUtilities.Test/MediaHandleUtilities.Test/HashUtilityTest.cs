using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaHandleUtilities.Test
{
	[TestClass]
	public class HashUtilityTest
	{
		/// <summary>
		/// Test the 2 files given to test the hash algorithm @ http://trac.opensubtitles.org/projects/opensubtitles/wiki/HashSourceCodes
		/// </summary>
		[TestMethod]
		public void ComputeMovieHashTest()
		{
			// breakdance.avi
			string breakdanceAviHash = HashUtility.ComputeMovieHash(@"HashUtilityTestItems\breakdance.avi");
			Assert.AreEqual("8e245d9679d31e12", breakdanceAviHash, "The hash generated for breakdance.avi did not match the expected hash.");
			
			// dummy.rar
			// commented out because while the packed .rar file (source control'ed) is 2 MB, the unpacked .bin file is 4 gb
			// if you would like to run this test, delete .bin file afterwards if you want to clean up wasted space

			/*string dummyRarHash = HashUtility.ComputeMovieHash(@"HashUtilityTestItems\dummy.bin");
			Assert.AreEqual("61F7751FC2A72BFB", dummyRarHash, "The hash generated for dummary.bin did not match the expected hash.");*/
		}
	}
}
