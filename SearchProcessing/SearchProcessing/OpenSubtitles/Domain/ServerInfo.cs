using CookComputing.XmlRpc;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class ServerInfo
	{
		[XmlRpcMember("xmlrpc_version")]
		public string XmlRpcVersionType { get; set; }

		[XmlRpcMember("xmlrpc_url")]
		public string XmlRpcUrl { get; set; }

		[XmlRpcMember("application")]
		public string Application { get; set; }

		[XmlRpcMember("contact")]
		public string Contact { get; set; }

		[XmlRpcMember("website_url")]
		public string WebsiteUrl { get; set; }

		[XmlRpcMember("users_online_total")]
		public int UsersOnlineTotal { get; set; }

		[XmlRpcMember("users_online_program")]
		public int UsersOnlineProgram { get; set; }

		[XmlRpcMember("users_loggedin")]
		public int UsersLoggedin { get; set; }

		[XmlRpcMember("users_max_alltime")]
		public string UsersMaxAlltime { get; set; }

		[XmlRpcMember("users_registered")]
		public string UsersRegistered { get; set; }

		[XmlRpcMember("subs_downloads")]
		public string SubsDownloads { get; set; }

		[XmlRpcMember("subs_subtitle_files")]
		public string SubsSubtitleFiles { get; set; }

		[XmlRpcMember("movies_total")]
		public string MoviesTotal { get; set; }

		[XmlRpcMember("movies_aka")]
		public string MoviesAka { get; set; }

		[XmlRpcMember("total_subtitles_languages")]
		public string TotalSubtitlesLanguages { get; set; }

		[XmlRpcMember("last_update_strings")]
		public XmlRpcStruct LastUpdateStrings { get; set; }
		
		[XmlRpcMember("download_limits")]
		public XmlRpcStruct DownloadLimits { get; set; }

		[XmlRpcMember("seconds")]
		public double Seconds { get; set; }
	}
}