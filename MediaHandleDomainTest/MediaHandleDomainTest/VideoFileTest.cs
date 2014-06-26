using MediaHandleDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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
