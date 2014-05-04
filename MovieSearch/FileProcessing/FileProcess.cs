using MediaHandleDomain;
using MediaHandleUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileProcessing
{
	public class FileProcess
	{
		#region Fields

		private List<string> _movieFileExtensions;

		private List<string> _videoDisplayResolutions;

		private string _directoryPath;

		#endregion Fields

		#region Constructor

		public FileProcess(string directoryPath)
		{
			_directoryPath = directoryPath;

			_movieFileExtensions = EnumUtilities.GetStringValuesExceptNone<MediaFileExtensionLookupId>();

			_videoDisplayResolutions = EnumUtilities.GetStringValuesExceptNone<MediaFileExtensionLookupId>();
		}

		#endregion Constructor

		#region Methods

		public List<VideoFile> GetVideoFiles()
		{
			// get the files in the directory with the correct file extension
			List<FileInfo> filesWithMatchingExtensions = GetFilesWithMatchingExtensions();

			// determine which of these files are samples
			IEnumerable<FileInfo> sampleFiles = GetProbableSampleFiles(filesWithMatchingExtensions);
			
			// get rid of the samples
			IEnumerable<FileInfo> videoFileInfos = filesWithMatchingExtensions.Except(sampleFiles);

			List<VideoFile> videoFiles = new List<VideoFile>();
			
			foreach (FileInfo videoFileInfo in videoFileInfos)
			{
				videoFiles.Add(ProcessFileInfo(videoFileInfo));
			}

			return videoFiles;
		}

		#endregion Methods

		#region Helper Methods

		public List<FileInfo> GetFilesWithMatchingExtensions()
		{
			DirectoryInfo moviesDirectoryInfo = new DirectoryInfo(_directoryPath);

			IEnumerable<FileInfo> allFiles = moviesDirectoryInfo.GetFiles("*", SearchOption.AllDirectories);

			return allFiles.Where(f => _movieFileExtensions.Any(f.Extension.Equals)).ToList();
		}

		public static IEnumerable<FileInfo> GetProbableSampleFiles(IEnumerable<FileInfo> movieFiles)
		{
			return movieFiles
				.Where(f => f.Name.Contains("sample", StringComparison.OrdinalIgnoreCase))
				.Where(i => (i.Length / 1024 / 1024) < 50);
		}

		public static int GetMatchingMediaFileExtension(string extension)
		{
			var extensionEnums = EnumUtilities.GetEnumValueList<MediaFileExtensionLookupId>();
			
			// now compare the extension to the StringValue of all the extensions; StringValue is key here
			// enum default value is the first element; which is 'None'
			MediaFileExtensionLookupId matchingEnum = extensionEnums
				.FirstOrDefault(
					i => EnumUtilities
						.GetStringValue(i)
						.Equals(extension, StringComparison.InvariantCultureIgnoreCase)
				);

			return (int)matchingEnum;
		}

		private VideoFile ProcessFileInfo(FileInfo fileInfo)
		{
			VideoFile videoFile = new VideoFile(fileInfo);

			// *********** Extension ***********
			videoFile.MediaFileExtensionLookupId = GetMatchingMediaFileExtension(fileInfo.Extension);

			// keep track of file name; adjust after processing portions
			string fileName = fileInfo.Name;

			// *********** Video Resolution ***********
			var resolutionEnums = EnumUtilities.GetEnumValueList<VideoDisplayResolutionLookupId>();
			
			VideoDisplayResolutionLookupId matchingDisplayId = resolutionEnums
					.FirstOrDefault(
						i => fileName.Contains
							(
								EnumUtilities.GetStringValue(i), StringComparison.InvariantCultureIgnoreCase
							)
					);

			if (matchingDisplayId != VideoDisplayResolutionLookupId.None)
			{
				fileName = fileName.Replace(EnumUtilities.GetStringValue(matchingDisplayId), string.Empty);
			}

			return videoFile;
		}

		#endregion Helper Methods
	}
}
