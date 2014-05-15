using CookComputing.XmlRpc;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class Token : BasicReturn
	{
		[XmlRpcMember("token")]
		public string Value { get; set; }
	}
}