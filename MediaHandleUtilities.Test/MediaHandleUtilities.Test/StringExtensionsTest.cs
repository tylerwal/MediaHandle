using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaHandleUtilities.Test
{
	[TestClass]
	public class StringExtensionsTest
	{
		[TestMethod]
		public void ContainsIgnoreCaseFoundTest()
		{
			const string testString = "The quick brown fox jumps over the lazy dog.  Was he fast? No he was not!  There were 2 beagles chasing him.";

			bool doesContain = testString.Contains("dog", StringComparison.InvariantCultureIgnoreCase);
			Assert.IsTrue(doesContain, "The string does contain 'dog'.");
			
			doesContain = testString.Contains("FAST?", StringComparison.InvariantCultureIgnoreCase);
			Assert.IsTrue(doesContain, "The string does contain 'fast?'.");

			doesContain = testString.Contains("2 beaGLes", StringComparison.InvariantCultureIgnoreCase);
			Assert.IsTrue(doesContain, "The string does contain '2 beagles'.");
		}

		[TestMethod]
		public void ContainsNotFoundTest()
		{
			const string testString = "The quick brown fox jumps over the lazy dog.  Was he fast? No he was not!  There were 2 beagles chasing him.";

			bool doesContain = testString.Contains("apple", StringComparison.InvariantCultureIgnoreCase);
			Assert.IsFalse(doesContain, "The string does NOT contain 'apple'.");

			doesContain = testString.Contains("3 beagles", StringComparison.InvariantCultureIgnoreCase);
			Assert.IsFalse(doesContain, "The string does NOT contain '3 bealges'.");
		}
	}
}
