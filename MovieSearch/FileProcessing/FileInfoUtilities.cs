using FileProcessing.Constants;
using System.Linq;
using SystemInterface.IO;

namespace FileProcessing
{
	/// <summary>
	/// Utilities for <see cref="IFileInfo"/> objects.
	/// </summary>
	public static class FileInfoUtilities
	{
		/// <summary>
		/// Checks if extension is video file.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		/// <returns>True if the file extension matches a known video file extension.</returns>
		public static bool CheckIfExtensionIsVideoFile(IFileInfo fileInfo)
		{
			return ProcessingConstants.VideoFileExtensionStrings.Any(e => e == fileInfo.Extension);
		}
	}
}