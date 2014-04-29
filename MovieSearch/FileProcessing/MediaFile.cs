using System.IO;

namespace FileProcessing
{
	public class MediaFile
	{
		#region Fields

		private FileInfo _fileInfo;

		private int _mediaFileExtensionLookupId;

		private int _videoDisplayResolutionLookupId;

		#endregion Fields

		#region Constructors

		public MediaFile(FileInfo fileInfo)
		{
			_fileInfo = fileInfo;
		}

		#endregion Constructors

		#region Properties

		public FileInfo FileInfo
		{
			get
			{
				return _fileInfo;
			}
			set
			{
				_fileInfo = value;
			}
		}

		public int MediaFileExtensionLookupId
		{
			get
			{
				return _mediaFileExtensionLookupId;
			}
			set
			{
				_mediaFileExtensionLookupId = value;
			}
		}

		public int VideoDisplayResolutionLookupId
		{
			get
			{
				return _videoDisplayResolutionLookupId;
			}
			set
			{
				_videoDisplayResolutionLookupId = value;
			}
		}
		
		#endregion Properties

		#region Methods



		#endregion Methods

		#region Helper Methods



		#endregion Helper Methods
	}
}