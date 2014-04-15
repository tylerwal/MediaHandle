﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHandleDomain
{
	public class VideoFileInfo
	{
		#region Fields

		private FileInfo _fileInfo;

		private int _mediaFileExtensionLookupId;

		private int _videoDisplayResolutionLookupId;

		#endregion Fields

		#region Constructor

		public VideoFileInfo()
		{
		}

		public VideoFileInfo(FileInfo fileInfo, int mediaFileExtensionLookupId, int videoDisplayResolutionLookupId)
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
			get { return _mediaFileExtensionLookupId; }
			set { _mediaFileExtensionLookupId = value; }
		}

		#endregion Properties

		#region Methods



		#endregion Methods

		#region Helper Methods



		#endregion Helper Methods
	}
}
