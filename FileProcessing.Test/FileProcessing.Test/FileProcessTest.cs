using System;
using System.IO;

using MediaHandleDomain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileProcessing.Test
{
	[TestClass]
	public class FileProcessTest
	{
		[TestMethod]
		public void GetMatchingMediaFileExtensionTest()
		{
			int expectedResult = (int)MediaFileExtensionLookupId.None;
			int actualResult = FileProcess.GetMatchingMediaFileExtension(".timo");
			Assert.AreEqual(expectedResult, actualResult);

			expectedResult = (int)MediaFileExtensionLookupId.Mkv;
			actualResult = FileProcess.GetMatchingMediaFileExtension(".mkv");
			Assert.AreEqual(expectedResult, actualResult);

			expectedResult = (int)MediaFileExtensionLookupId.Avi;
			actualResult = FileProcess.GetMatchingMediaFileExtension(".avi");
			Assert.AreEqual(expectedResult, actualResult);

			expectedResult = (int)MediaFileExtensionLookupId.Mpg;
			actualResult = FileProcess.GetMatchingMediaFileExtension(".mpg");
			Assert.AreEqual(expectedResult, actualResult);

			expectedResult = (int)MediaFileExtensionLookupId.Wmv;
			actualResult = FileProcess.GetMatchingMediaFileExtension(".wmv");
			Assert.AreEqual(expectedResult, actualResult);

			expectedResult = (int)MediaFileExtensionLookupId.Mp4;
			actualResult = FileProcess.GetMatchingMediaFileExtension(".mp4");
			Assert.AreEqual(expectedResult, actualResult);
			
			// test without period before extension
			expectedResult = (int)MediaFileExtensionLookupId.Mp4;
			actualResult = FileProcess.GetMatchingMediaFileExtension("mp4");
			Assert.AreNotEqual(expectedResult, actualResult);

			// test case sensitivity
			expectedResult = (int)MediaFileExtensionLookupId.Mp4;
			actualResult = FileProcess.GetMatchingMediaFileExtension(".Mp4");
			Assert.AreEqual(expectedResult, actualResult);
		}
	}
}
