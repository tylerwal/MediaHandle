using CookComputing.XmlRpc;

using SearchProcessing.OpenSubtitles.Domain;

namespace SearchProcessing.OpenSubtitles
{
	[XmlRpcUrl("http://api.opensubtitles.org/xml-rpc")]
	public interface IOpenSubtitlesProxy : IXmlRpcProxy
	{
		[XmlRpcMethod("ServerInfo")]
		ServerInfo ServiceInfo();
	}
}