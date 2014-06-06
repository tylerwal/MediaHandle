using CookComputing.XmlRpc;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class LogInResponse : BasicResponse
	{
		[XmlRpcMember("token")]
		public string Token { get; set; }
	}
}