using CookComputing.XmlRpc;
using SearchProcessing.Annotations;

namespace SearchProcessing.OpenSubtitles.Domain
{
	[UsedImplicitly]
	public class LogInResponse : BasicResponse
	{
		[XmlRpcMember("token")]
		public string Token { get; set; }
	}
}