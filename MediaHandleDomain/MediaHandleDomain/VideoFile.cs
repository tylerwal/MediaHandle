using System.IO;
namespace MediaHandleDomain
{
	public class VideoFile
	{
		#region Fields

		private FileInfo _fileInfo;

		private int _mediaFileExtensionLookupId;

		private int _videoDisplayResolutionLookupId;

		private int? _year;

		private string _name;

		#endregion Fields

		#region Constructor

		public VideoFile()
		{
		}

		public VideoFile(FileInfo fileInfo)
		{
			_fileInfo = fileInfo;
		}

		public VideoFile(FileInfo fileInfo, int mediaFileExtensionLookupId)
		{
			_fileInfo = fileInfo;
			_mediaFileExtensionLookupId = mediaFileExtensionLookupId;
		}

		public VideoFile(FileInfo fileInfo, int mediaFileExtensionLookupId, int videoDisplayResolutionLookupId)
		{
			_fileInfo = fileInfo;
			_mediaFileExtensionLookupId = mediaFileExtensionLookupId;
			_videoDisplayResolutionLookupId = videoDisplayResolutionLookupId;
		}

		#endregion Constructor

		#region Properties

		public FileInfo FileInfo
		{
			get { return _fileInfo; }
			set { _fileInfo = value; }
		}

		public int MediaFileExtensionLookupId
		{
			get { return _mediaFileExtensionLookupId; }
			set { _mediaFileExtensionLookupId = value; }
		}

		public int VideoDisplayResolutionLookupId
		{
			get { return _videoDisplayResolutionLookupId; }
			set { _videoDisplayResolutionLookupId = value; }
		}

		public int? Year
		{
			get { return _year;  }
			set { _year = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		#endregion Properties

		#region Methods



		#endregion Methods

		#region Helper Methods



		#endregion Helper Methods
	}
}
