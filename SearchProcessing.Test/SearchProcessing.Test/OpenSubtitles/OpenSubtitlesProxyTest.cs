using CookComputing.XmlRpc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles;
using SearchProcessing.OpenSubtitles.Domain;

namespace SearchProcessing.Test.OpenSubtitles
{
	[TestClass]
	public class OpenSubtitlesProxyTest
	{
		/// <summary>
		/// Test to make sure the XMLRPC connection is working with the most basic method on OpenSubtitles: ServerInfo()
		/// 
		/// ServerInfo() does not require a login;
		/// </summary>
		[TestMethod]
		public void ServerInfoTest()
		{
			IOpenSubtitlesProxy proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();

			ServerInfo serviceInfo = proxy.ServiceInfo();

			Assert.AreEqual("http://api.opensubtitles.org/xml-rpc", serviceInfo.XmlRpcUrl, "The ServerInfo() method did not returned the expected xmlrpc url.");

			Assert.AreEqual("http://www.opensubtitles.org", serviceInfo.WebsiteUrl, "The ServerInfo() method did not returned the expected website url.");
		}
	}
}
