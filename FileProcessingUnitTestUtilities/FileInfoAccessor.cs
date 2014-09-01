using System;
using System.IO;
using System.Security.AccessControl;
using SystemInterface;
using SystemInterface.IO;
using SystemInterface.Security.AccessControl;

namespace FileProcessingUnitTestUtilities
{
	public class FileInfoAccessor : IFileInfo
	{
		public void Initialize(FileInfo fileInfo)
		{
			throw new NotImplementedException();
		}

		public void Initialize(string fileName)
		{
			throw new NotImplementedException();
		}

		public FileAttributes Attributes
		{
			get;
			set;
		}

		public IDateTime CreationTime
		{
			get;
			set;
		}

		public IDateTime CreationTimeUtc
		{
			get;
			set;
		}

		public IDirectoryInfo Directory
		{
			get;
			set;
		}

		public string DirectoryName
		{
			get;
			set;
		}

		public bool Exists
		{
			get;
			set;
		}

		public string Extension
		{
			get;
			set;
		}

		public FileInfo FileInfoInstance
		{
			get;
			set;
		}

		public string FullName
		{
			get;
			set;
		}

		public bool IsReadOnly
		{
			get;
			set;
		}

		public IDateTime LastAccessTime
		{
			get;
			set;
		}

		public IDateTime LastAccessTimeUtc
		{
			get;
			set;
		}

		public IDateTime LastWriteTime
		{
			get;
			set;
		}

		public IDateTime LastWriteTimeUtc
		{
			get;
			set;
		}

		public long Length
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public IStreamWriter AppendText()
		{
			throw new NotImplementedException();
		}

		public IFileInfo CopyTo(string destFileName)
		{
			throw new NotImplementedException();
		}

		public IFileInfo CopyTo(string destFileName, bool overwrite)
		{
			throw new NotImplementedException();
		}

		public IFileStream Create()
		{
			throw new NotImplementedException();
		}

		public IStreamWriter CreateText()
		{
			throw new NotImplementedException();
		}

		public void Decrypt()
		{
			throw new NotImplementedException();
		}

		public void Delete()
		{
			throw new NotImplementedException();
		}

		public void Encrypt()
		{
			throw new NotImplementedException();
		}

		public IFileSecurity GetAccessControl()
		{
			throw new NotImplementedException();
		}

		public IFileSecurity GetAccessControl(AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		public void MoveTo(string destFileName)
		{
			throw new NotImplementedException();
		}

		public IFileStream Open(FileMode mode)
		{
			throw new NotImplementedException();
		}

		public IFileStream Open(FileMode mode, FileAccess access)
		{
			throw new NotImplementedException();
		}

		public IFileStream Open(FileMode mode, FileAccess access, FileShare share)
		{
			throw new NotImplementedException();
		}

		public IFileStream OpenRead()
		{
			throw new NotImplementedException();
		}

		public IStreamReader OpenText()
		{
			throw new NotImplementedException();
		}

		public IFileStream OpenWrite()
		{
			throw new NotImplementedException();
		}

		public void Refresh()
		{
			throw new NotImplementedException();
		}

		public IFileInfo Replace(string destinationFileName, string destinationBackupFileName)
		{
			throw new NotImplementedException();
		}

		public IFileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			throw new NotImplementedException();
		}

		public void SetAccessControl(IFileSecurity fileSecurity)
		{
			throw new NotImplementedException();
		}
	}
}
