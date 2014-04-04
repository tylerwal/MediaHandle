using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace MediaHandleConsole
{
	class Program
	{
		private List<string> mediaFileExtensions = new List<string>
			{
				".mkv",
				".avi",
				".mpg",
				".wmv",
				".mp4"
			};	

		static void Main(string[] args)
		{
			Program prog = new Program();

			prog.DoStuff();
		}

		private void DoStuff()
		{
			DirectoryInfo moviesDirectoryInfo = new DirectoryInfo(@"\\SERVER\Downloads\Movies\");

			var fileSystemInfos = moviesDirectoryInfo.EnumerateFileSystemInfos("*", SearchOption.AllDirectories);

			//var files = moviesDirectoryInfo.EnumerateFiles("*", SearchOption.AllDirectories);

			var fileList = moviesDirectoryInfo.GetFiles("*", SearchOption.AllDirectories).ToList();

			var matches =
				from fileInfo in fileList
				from mediaFileExtension in mediaFileExtensions
				where fileInfo.Name.Contains(mediaFileExtension)
				select fileInfo;

			var m = matches.Count();

			var betterMatches = fileList
				.Where(f =>
				{
					bool isMatch = false;

					foreach (string mediaFileExtension in mediaFileExtensions)
					{
						if (f.Name.Contains(mediaFileExtension))
						{
							isMatch = true;
						}
					}

					return isMatch;
				});

			var bm = betterMatches.Count();

			var optoMatches = fileList.Where(f => mediaFileExtensions.Any(f.Name.Contains));

			var om = optoMatches.Count();

			var eventBetter = fileList.Where(f => mediaFileExtensions.Any(f.Extension.Equals));

			var eb = eventBetter.Count();

			var mediaSystemFiles = fileSystemInfos.Where(i => i.Name.Contains(mediaFileExtensions.First()));
			
			//var mediaFiles = files.Where(i => i.Name.Contains(mediaFileExtensions.First()));
		}
	}
}
