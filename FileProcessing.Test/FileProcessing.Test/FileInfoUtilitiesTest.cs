
using System.IO;
using System.Security.AccessControl;

using SystemInterface;
using SystemInterface.IO;
using SystemInterface.Security.AccessControl;

using FileProcessingUnitTestUtilities;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemWrapper.IO;

namespace FileProcessing.Test
{
	/// <summary>
	/// Tests for the <see cref="FileInfoUtilities"/> class.
	/// </summary>
	[TestClass]
	public class FileInfoUtilitiesTest
	{
		/// <summary>
		/// Tests the CheckIfExtensionIsVideoFile method when the return value is true.
		/// </summary>
		[TestMethod]
		public void CheckIfExtensionIsVideoFileTrueTest()
		{
			FileInfoAccessor fileInfo = new FileInfoAccessor
			{
				Extension = ".mkv"
			};

			bool isVideoFile = FileInfoUtilities.CheckIfExtensionIsVideoFile(fileInfo);

			Assert.IsTrue(isVideoFile, "The '.mkv' extension should have registered as a video file.");
		}

		/// <summary>
		/// Tests the CheckIfExtensionIsVideoFile method when the return value is false.
		/// </summary>
		[TestMethod]
		public void CheckIfExtensionIsVideoFileFalseTest()
		{
			FileInfoAccessor fileInfo = new FileInfoAccessor
			{
				Extension = ".exe"
			};

			bool isVideoFile = FileInfoUtilities.CheckIfExtensionIsVideoFile(fileInfo);

			Assert.IsFalse(isVideoFile, "The '.exe' extension should NOT have registered as a video file.");
		}
	}
}
