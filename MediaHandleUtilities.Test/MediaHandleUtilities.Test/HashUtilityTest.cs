using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaHandleUtilities.Test
{
	[TestClass]
	public class HashUtilityTest
	{
		[TestMethod]
		public void ComputeMovieHashTest()
		{
			var actualHash = HashUtility.ComputeMovieHash(@"C:\Programming\Media Handle\MediaHandle\MediaHandleUtilities.Test\dummy.rar");

			var actualHashHex = HashUtility.ToHexadecimal(actualHash);


		}
	}
}
