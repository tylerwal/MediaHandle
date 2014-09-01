using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace FileProcessing
{
	public static class DirectoryInfoUtilities
	{
		public static List<IFileInfo> GetAllFiles(string directoryPath)
		{
			DirectoryInfoWrap directoryInfo = new DirectoryInfoWrap(directoryPath);

			return directoryInfo.GetFiles("*", SearchOption.AllDirectories).ToList();
		}
	}
}