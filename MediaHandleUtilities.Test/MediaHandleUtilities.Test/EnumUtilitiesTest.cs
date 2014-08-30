using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MediaHandleUtilities.Test
{
	[TestClass]
	public class EnumUtilitiesTest
	{
		/// <summary>
		/// Tests the GetMatchingEnum method when a match is found.
		/// </summary>
		[TestMethod]
		public void GetMatchingEnumTest()
		{
			VideoDisplayResolutionLookupId actual = EnumUtilities.GetMatchingEnum<VideoDisplayResolutionLookupId>("480p");

			Assert.AreEqual(VideoDisplayResolutionLookupId.FourEightyPixels, actual, "The expected Enum value did not match according to the string passed in.");
		}

		/// <summary>
		/// Tests the GetMatchingEnum method when no match is found.
		/// </summary>
		[TestMethod]
		public void GetMatchingEnumNoMatchTest()
		{
			VideoDisplayResolutionLookupId actual = EnumUtilities.GetMatchingEnum<VideoDisplayResolutionLookupId>("notFound");

			Assert.AreEqual(VideoDisplayResolutionLookupId.None, actual, "There should not have been a match found - which meant it should have defaulted to 'None'.");
		}

		/// <summary>
		/// Tests the GetEnumValueList method.
		/// </summary>
		[TestMethod]
		public void GetEnumValueListTest()
		{
			IEnumerable<VideoDisplayResolutionLookupId> actualList = EnumUtilities.GetEnumValueList<VideoDisplayResolutionLookupId>().ToList();

			List<VideoDisplayResolutionLookupId> expectedList = new List<VideoDisplayResolutionLookupId>
			{
				VideoDisplayResolutionLookupId.None,
				VideoDisplayResolutionLookupId.FourEightyPixels,
				VideoDisplayResolutionLookupId.SevenTwentyPixels,
				VideoDisplayResolutionLookupId.TenEightyPixels
			};

			Assert.AreEqual(actualList.Count(), expectedList.Count, "The actual list and the expected list did not match in size.");

			for (int i = 0; i < expectedList.Count; i++)
			{
				Assert.AreEqual(expectedList[i], actualList.ElementAt(i), "The enum values did not match.");
			}
		}

		/// <summary>
		/// Tests the GetStringValues method.
		/// </summary>
		[TestMethod]
		public void GetStringValuesTest()
		{
			List<string> actualList = EnumUtilities.GetStringValues<VideoDisplayResolutionLookupId>();

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

		/// <summary>
		/// Tests the GetStringValuesExceptNone method.
		/// </summary>
		[TestMethod]
		public void GetStringValuesExceptNoneTest()
		{
			List<string> actualList = EnumUtilities.GetStringValuesExceptNone<VideoDisplayResolutionLookupId>();

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

		/// <summary>
		/// Tests the GetStringValue method.
		/// </summary>
		[TestMethod]
		public void GetStringValueTest()
		{
			string actual = EnumUtilities.GetStringValue(FooBar.Bizz);

			Assert.AreEqual("Bizz", actual, "The string value returned did not match the expected value from the StringValueAttribute.");
		}

		/// <summary>
		/// Tests the GetStringValue method.
		/// </summary>
		[TestMethod]
		public void GetStringValueNullTest()
		{
			string actual = EnumUtilities.GetStringValue(FooBar.Bazz);

			Assert.IsNull(actual, "The string value returned was not null.");
		}

		#region Test Enums

		private enum VideoDisplayResolutionLookupId
		{
			[StringValue("None")]
			None = 0,

			[StringValue("480p")]
			FourEightyPixels = 1,

			[StringValue("720p")]
			SevenTwentyPixels = 2,

			[StringValue("1080p")]
			TenEightyPixels = 3
		}

		private enum FooBar
		{
			[StringValue("Bizz")]
			Bizz,

			Bazz
		}

		#endregion Test Enums
	}
}
