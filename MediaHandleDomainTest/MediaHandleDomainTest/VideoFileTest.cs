using System;
using System.IO;

using MediaHandleDomain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaHandleDomainTest
{
	[TestClass]
	public class VideoFileTest
	{
		[TestMethod]
		public void ConstructorTest()
		{
			FileInfo fileInfo = new FileInfo("test");

			VideoFile videoFile = new VideoFile();

		}
	}
}
