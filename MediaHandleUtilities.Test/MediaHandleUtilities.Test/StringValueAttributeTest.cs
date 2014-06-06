using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaHandleUtilities.Test
{
	[TestClass]
	public class StringValueAttributeTest
	{
		[TestMethod]
		public void StringValueAttributeIntegrationTest()
		{
			string outputString = EnumUtilities.GetStringValue(DummyEnum.Red);

			string expectedString = Enum.GetName(typeof(DummyEnum), DummyEnum.Red);

			Assert.AreEqual(expectedString, outputString, "The returned value of the enum did not match the expected output.");

			string outputEmptyString = EnumUtilities.GetStringValue(DummyEnum.Blue);
			Assert.AreEqual(string.Empty, outputEmptyString, "The returned value of the enum with an empty string was not an empty string.");

			string outputNullString = EnumUtilities.GetStringValue(DummyEnum.Yellow);
			Assert.IsNull(outputNullString, "The returned value of the enum with no StringValue was not null.");
		}

		#region Helper Enum

		private enum DummyEnum
		{
			[StringValue("Red")]
			Red = 1,

			[StringValue("")]
			Blue = 2,

			Yellow = 3,
		}

		#endregion Helper Enum
	}
}
