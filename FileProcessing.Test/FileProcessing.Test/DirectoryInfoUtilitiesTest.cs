using System;
using System.Collections.Generic;

using SystemInterface.IO;

using SystemWrapper.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FileProcessing.Test
{
	/// <summary>
	/// Tests for the <see cref="DirectoryInfoUtilities"/> class.
	/// </summary>
	[TestClass]
	public class DirectoryInfoUtilitiesTest
	{
		/// <summary>
		/// Tests the GetAllFiles method.
		/// </summary>
		[TestMethod]
		public void GetAllFilesTest()
		{
			const string testDirectory = @"C:\TestDirectory\";

			bool doesDirectoryAlreadyExist = Directory.Exists(testDirectory);

			if (doesDirectoryAlreadyExist)
			{
				throw new Exception("The test directory already exists; this unit test deletes the directory afterwards so proceeding could result in data loss.");
			}
			
			List<string> directories = new List<string>
			{
				testDirectory
			};

			List<string> files = new List<string>();

			try
			{
				#region Test Directory Creation
			
				string levelOneFileA = Path.Combine(testDirectory, "levelOneFileA.doc");
				files.Add(levelOneFileA);

				string levelOneFileB = Path.Combine(testDirectory, "levelOneFileB.mov");
				files.Add(levelOneFileB);

				string levelOneDirectory = Path.Combine(testDirectory, @"levelOne\");
				directories.Add(levelOneDirectory);

					string levelTwoFileA = Path.Combine(levelOneDirectory, "levelTwoFileA.doc");
					files.Add(levelTwoFileA);

					string levelTwoFileB = Path.Combine(levelOneDirectory, "levelTwoFileB.mov");
					files.Add(levelTwoFileB);

					string levelTwoDirectory = Path.Combine(levelOneDirectory, @"levelTwo\");
					directories.Add(levelTwoDirectory);

						string levelThreeFileA = Path.Combine(levelTwoDirectory, "levelThreeFileA.doc");
						files.Add(levelThreeFileA);

						string levelThreeFileB = Path.Combine(levelTwoDirectory, "levelThreeFileB.mov");
						files.Add(levelThreeFileB);

				foreach (string directory in directories)
				{
					Directory.CreateDirectory(directory);
				}

				foreach (string file in files)
				{
					File.WriteAllText(file, "data");
				}

				#endregion Test Directory Creation

				List<IFileInfo> fileList = DirectoryInfoUtilities.GetAllFiles(testDirectory);

				Assert.AreEqual(6, fileList.Count, "The correct number of files was not found.");
			}
			finally
			{
				#region Test Directory Deletion

				Directory.Delete(testDirectory, true);

				#endregion Test Directory Deletion		
			}
		}
	}
}
