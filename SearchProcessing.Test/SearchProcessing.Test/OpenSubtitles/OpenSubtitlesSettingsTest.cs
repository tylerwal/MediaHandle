using System.Runtime.Remoting.Channels;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace SearchProcessing.Test.OpenSubtitles
{
	[TestClass]
	public class OpenSubtitlesSettingsTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var test = ConfigurationManager.AppSettings.Get("Username");

			var test2 = SearchProcessing.OpenSubtitles.OpenSubtitlesSettings.Default;
		}
	}
}
