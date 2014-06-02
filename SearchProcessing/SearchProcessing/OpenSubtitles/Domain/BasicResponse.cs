using CookComputing.XmlRpc;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class BasicResponse
	{
		[XmlRpcMember("status")]
		public string Status { get; set; }

		[XmlRpcMember("seconds")]
		public double Seconds { get; set; }
	}
}