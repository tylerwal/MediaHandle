using CookComputing.XmlRpc;
using SearchProcessing.OpenSubtitles.Domain;

namespace SearchProcessing.OpenSubtitles
{
	/// <summary>
	/// Documenation @ http://trac.opensubtitles.org/projects/opensubtitles/wiki/XMLRPC
	/// </summary>
	[XmlRpcUrl("http://api.opensubtitles.org/xml-rpc")]
	public interface IOpenSubtitlesProxy : IXmlRpcProxy
	{
		/// <summary>
		/// This simple function returns basic server info, it could be used for ping or telling server info to client, no valid 
		/// UserAgent is needed, so it is also good for testing XML-RPC. 
		/// </summary>
		/// <returns>Array of server information.</returns>
		[XmlRpcMethod("ServerInfo")]
		ServerInfo ServiceInfo();

		/// <summary>
		/// This will login user. This function should be called always when starting talking with server. It returns token, which must be used in later 
		/// communication. If user has no account, blank username and password should be OK. As language - use ​ISO639 2 letter code and later 
		/// communication will be done in this language if applicable (error codes and so on). Note: when username and password is blank, status is 200 OK, 
		/// because we want allow anonymous users too. Useragent cannot be empty string. For $useragent use your registered useragent, also provide version 
		/// number - we need tracking version numbers of your program. If your UA is not registered, you will get error 414 Unknown User Agent. 
		/// </summary>
		/// <param name="username">Username</param>
		/// <param name="password">User password</param>
		/// <param name="language">Letter code (en = english)</param>
		/// <param name="userAgent">Registered User Agent</param>
		/// <returns></returns>
		[XmlRpcMethod("LogIn")]
		LogInResponse LogIn(string username, string password, string language, string userAgent);

		/// <summary>
		/// This will logout user (ends session id). Good call this function is before ending (closing) clients program. 
		/// </summary>
		/// <param name="token">Session Token</param>
		/// <returns>Status and Seconds</returns>
		[XmlRpcMethod("LogOut")]
		BasicResponse LogOut(string token);

		/// <summary>
		/// This function should be called each 15 minutes after last request to xmlrpc. It is used for not expiring current session. 
		/// It also returns if current $token is registered. 
		/// </summary>
		/// <param name="token">Session Token</param>
		/// <returns>Status and Seconds</returns>
		[XmlRpcMethod("NoOperation")]
		BasicResponse SessionCheck(string token);

		[XmlRpcMethod("CheckMovieHash")]
		SearchByHashResponse SearchByHash(string token, string[] fileHash);
	}
}