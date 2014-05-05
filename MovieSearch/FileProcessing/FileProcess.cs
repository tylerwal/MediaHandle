using MediaHandleDomain;
using MediaHandleUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileProcessing
{
	public class FileProcess
	{
		#region Fields

		private readonly List<string> _movieFileExtensions;

		private readonly List<string> _videoDisplayResolutions;

		private string _directoryPath;

		private readonly List<string> _commonWordsToRemove; 

		#endregion Fields

		#region Constructor

		public FileProcess(string directoryPath)
		{
			_directoryPath = directoryPath;

			_movieFileExtensions = EnumUtilities.GetStringValuesExceptNone<MediaFileExtensionLookupId>();

			_videoDisplayResolutions = EnumUtilities.GetStringValuesExceptNone<MediaFileExtensionLookupId>();

			_commonWordsToRemove = new List<string>
			{
				"BluRay",
				"x264",
				"XVID",
				"AC3",
				"DVDScr"
			};
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

			// keep track of file name; adjust after processing portions
			string fileName = fileInfo.Name;

			// *********** Extension ***********
			videoFile.MediaFileExtensionLookupId = GetMatchingMediaFileExtension(fileInfo.Extension);

			int fileNameLength = fileInfo.Name.Length;
			int extensionLength = fileInfo.Extension.Length;

			fileName = fileName.Substring(0, fileNameLength - extensionLength);

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

			// *********** Year ***********
			Regex yearRegex = new Regex(@"\d{4}");

			Match yearMatch = yearRegex.Match(fileName);

			if (yearMatch.Success)
			{
				// guaranteed to be an int because of the regex match
				videoFile.Year = int.Parse(yearMatch.Value);

				fileName = fileName.Replace(videoFile.Year.ToString(), string.Empty);
			}

			// *********** Remove garbage words ***********
			foreach (string word in _commonWordsToRemove)
			{
				fileName = fileName.Replace(word, string.Empty);
			}

			// *********** Clean up remaining string ***********

			// get text that comes after 2 periods
			Regex garbageRegex = new Regex(@"\.{2,}[\w\.\-]+");

			fileName = garbageRegex.Replace(fileName, string.Empty);

			// *********** Convert periods to spaces ***********

			videoFile.Name = fileName.Replace(".", " ");

			return videoFile;
		}

		#endregion Helper Methods
	}
}
