using CookComputing.XmlRpc;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class SearchByHashResponse : BasicResponse
	{
		[XmlRpcMember("data")]
		public XmlRpcStruct MediaData { get; private set; }
	}
}