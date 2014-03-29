using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MediaHandleConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Program prog = new Program();

			prog.DoStuff();
		}

		private void DoStuff()
		{
			DirectoryInfo moviesDirectoryInfo = new DirectoryInfo(@"\\SERVER\Downloads\Movies\");

			var fileSystemInfos = moviesDirectoryInfo.EnumerateFileSystemInfos();

			var files = moviesDirectoryInfo.EnumerateFiles("*", SearchOption.AllDirectories);
		}
	}
}
