using MediaHandleDomain;
using MediaHandleUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		    _movieFileExtensions = EnumUtilities.GetStringValuesExceptNone(typeof(MediaFileExtensionLookupId));

		    _videoDisplayResolutions = EnumUtilities.GetStringValuesExceptNone(typeof(VideoDisplayResolutionLookupId));
	    }

	    #endregion Constructor

	    #region Methods

	    public List<FileInfo> GetMovieFiles()
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

	    #endregion Methods

	    #region Helper Methods

	     

	    #endregion Helper Methods
    }
}
