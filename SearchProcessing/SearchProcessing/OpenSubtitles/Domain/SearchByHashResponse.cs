using CookComputing.XmlRpc;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class SearchByHashResponse : BasicResponse
	{
		/*[XmlRpcMember("data")]
		public MovieResponse MediaData { get; set; }*/

		[XmlRpcMember("data")]
		public XmlRpcStruct MediaData { get; private set; }

		/*[XmlRpcMember("data")]
		public object[] MediaData { get; set; }*/
	}
}