using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MediaHandleUtilities.Test
{
	[TestClass]
	public class EnumUtilitiesTest
	{
		[TestMethod]
		public void GetStringValuesTest()
		{
			List<string> actualList = EnumUtilities.GetStringValues(typeof(VideoDisplayResolutionLookupId));

			List<string> expectedList = new List<string>
			{
				"None",
				"480p",
				"720p",
				"1080p"
			};

			Assert.AreEqual(actualList.Count, expectedList.Count, "The actual list and the expected list did not match in size.");

			for (int i = 0; i < expectedList.Count; i++)
			{
				Assert.AreEqual(expectedList[i], actualList[i], "The enum string values did not match.");
			}
		}

		[TestMethod]
		public void GetStringValuesExceptNoneTest()
		{
			List<string> actualList = EnumUtilities.GetStringValuesExceptNone(typeof(VideoDisplayResolutionLookupId));

			List<string> expectedList = new List<string>
			{
				"480p",
				"720p",
				"1080p"
			};

			Assert.AreEqual(actualList.Count, expectedList.Count, "The actual list and the expected list did not match in size.");

			for (int i = 0; i < expectedList.Count; i++)
			{
				Assert.AreEqual(expectedList[i], actualList[i], "The enum string values did not match.");
			}
		}
	}

	public enum VideoDisplayResolutionLookupId
	{
		[StringValue("None")]
		None = 0,

		[StringValue("480p")]
		FourEightyPixels = 1,

		[StringValue("720p")]
		SevenTwentyPixels = 2,

		[StringValue("1080p")]
		TenEightyPixels = 3,
	}
}
