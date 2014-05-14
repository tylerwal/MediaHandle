using CookComputing.XmlRpc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles;

namespace SearchProcessing.Test.OpenSubtitles
{
	[TestClass]
	public class OpenSubtitlesProxyTest
	{
		[TestMethod]
		public void ServerInfoTest()
		{
			IOpenSubtitlesProxy proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();

			var test = proxy.ServiceInfo();
		}
	}
}
