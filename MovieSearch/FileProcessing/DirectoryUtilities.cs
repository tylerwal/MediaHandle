using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileProcessing
{
	public static class DirectoryUtilities
	{
		private static List<FileInfo> GetAllFiles(string directoryPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

			return directoryInfo.GetFiles("*", SearchOption.AllDirectories).ToList();
		}
	}
}