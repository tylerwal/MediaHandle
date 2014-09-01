using MediaHandleDomain;
using MediaHandleUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FileProcessing.Constants;

namespace FileProcessing
{
	public class FileProcess
	{
		#region Fields

		private readonly string _directoryPath;

		#endregion Fields

		#region Constructor

		public FileProcess(string directoryPath)
		{
			_directoryPath = directoryPath;
		}

		#endregion Constructor

		#region Methods

		public IEnumerable<VideoFile> GetVideoFiles()
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

		private List<FileInfo> GetFilesWithMatchingExtensions()
		{
			DirectoryInfo moviesDirectoryInfo = new DirectoryInfo(_directoryPath);

			IEnumerable<FileInfo> allFiles = moviesDirectoryInfo.GetFiles("*", SearchOption.AllDirectories);

			return allFiles.Where(f => ProcessingConstants.VideoFileExtensionStrings.Any(f.Extension.Equals)).ToList();
		}

		/// <summary>
		/// Creates a list of files that are probably samples based on the name and file size.
		/// Considers a file sampel if it has the word 'sample' and is under 50mb.
		/// </summary>
		/// <param name="movieFiles">List of all movie files.</param>
		/// <returns>List of probably sample movie files.</returns>
		private static IEnumerable<FileInfo> GetProbableSampleFiles(IEnumerable<FileInfo> movieFiles)
		{
			return movieFiles
				.Where(f => f.Name.Contains("sample", StringComparison.OrdinalIgnoreCase))
				.Where(i => (i.Length / 1024 / 1024) < 50);
		}

		/// <summary>
		/// Returns the MediaFileExtensionLookupId that corresponds to the file extension.
		/// </summary>
		/// <param name="extension">The file extension - including the ".".</param>
		/// <returns>The MediaFileExtensionLookupId int value.</returns>
		internal static int GetMatchingMediaFileExtension(string extension)
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
			VideoDisplayResolutionLookupId matchingDisplayId = ProcessingConstants.VideoDisplayResolutionEnums
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
			Regex yearRegex = new Regex(@"\d{4}", RegexOptions.Compiled);

			MatchCollection yearMatches = yearRegex.Matches(fileName);

			// only looks for years within the range of 1900 to 2050
			int? yearWithinRange = yearMatches
				.Cast<Match>()
				.Select(i => int.Parse(i.Value))
				.Cast<int?>()
				.FirstOrDefault(y => y > 1900 && y < 2050);

			if (yearWithinRange.HasValue)
			{
				videoFile.Year = yearWithinRange.Value;

				fileName = fileName.Replace(yearWithinRange.ToString(), string.Empty);
			}
			
			// *********** Remove garbage words ***********
			foreach (string word in ProcessingConstants.JunkWordsToIgnore)
			{
				fileName = fileName.Replace(word, string.Empty);
			}

			// *********** Clean up remaining string ***********

			// get rid of empty parentheses
			fileName = fileName.Replace(@"()", string.Empty);

			// get rid of things remaining in square brackets
			Regex bracketRegex = new Regex(@"\[.*?\]", RegexOptions.Compiled);

			fileName = bracketRegex.Replace(fileName, string.Empty);

			// common tag by a group
			Regex groupRegex = new Regex(@"-\w+$", RegexOptions.Compiled);

			fileName = groupRegex.Replace(fileName, string.Empty);

			// get text that comes after 2 periods
			Regex endJunk = new Regex(@"[\.\s]{2,}.*", RegexOptions.Compiled);

			fileName = endJunk.Replace(fileName, string.Empty);

			// *********** Convert periods to spaces ***********

			videoFile.Name = fileName.Replace(".", " ");

			return videoFile;
		}

		#endregion Helper Methods
	}
}
