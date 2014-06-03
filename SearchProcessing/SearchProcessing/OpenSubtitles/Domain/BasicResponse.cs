using System.Linq;

using CookComputing.XmlRpc;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class BasicResponse
	{
		#region Properties

		[XmlRpcMember("status")]
		public string Status { get; set; }

		[XmlRpcMember("seconds")]
		public double Seconds { get; set; }

		#endregion Properties

		#region Constructor

		 

		#endregion Constructor

		#region Methods

		public ResponseStatusLookupId GetResponseStatus()
		{
			string statusString = Status.Substring(0, 3);

			int statusCode;

			bool isParsable = int.TryParse(statusString, out statusCode);

			if (isParsable)
			{
				return (ResponseStatusLookupId)statusCode;
			}
			else
			{
				return ResponseStatusLookupId.None;
			}
		}

		#endregion Methods
	}
}