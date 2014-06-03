using MediaHandleUtilities;

namespace SearchProcessing.OpenSubtitles.Domain
{
	/// <summary>
	/// Documentation @ http://trac.opensubtitles.org/projects/opensubtitles/wiki/XMLRPC#Statuscodes
	/// </summary>
	public enum ResponseStatusLookupId
	{
		//[StringValue("None")]
		None = 0,

		#region Successful 2xx

		/// <summary>
		/// OK
		/// </summary>
		//[StringValue("200 OK")]
		Ok = 200,

		/// <summary>
		/// Partial content; message 
		/// </summary>
	//	[StringValue("206 Partial content")]
		OkPartial = 206,

		#endregion Successful 2xx

		#region Moved 3xx

		/// <summary>
		/// Moved (host) 
		/// </summary>
		HostMoved = 301,

		#endregion Moved 3xx

		#region Errors 4xx

		/// <summary>
		/// Unauthorized
		/// </summary>
	//	[StringValue("401 Unauthorized")]
		Unauthorized = 401,

		/// <summary>
		/// Subtitles has invalid format 
		/// </summary>
		InvalidSubtitlesFormat = 402,

		/// <summary>
		/// SubHashes (content and sent subhash) are not same! 
		/// </summary>
		SubHashesNotMatch = 403,

		/// <summary>
		/// Subtitles has invalid language! 
		/// </summary>
		InvalidSubtitleLanguage = 404,

		/// <summary>
		/// Not all mandatory parameters was specified 
		/// </summary>
		MissingParameters = 405,

		/// <summary>
		/// No session 
		/// </summary>
		NoSession = 406,

		/// <summary>
		/// Download limit reached 
		/// </summary>
		DownloadLimitReached = 407,

		/// <summary>
		/// Invalid parameters 
		/// </summary>
		InvalidParameters = 408,

		/// <summary>
		/// Method not found 
		/// </summary>
		MethodNotFound = 409,

		/// <summary>
		/// Other or unknown error 
		/// </summary>
		UnknownError = 410,

		/// <summary>
		/// Empty or invalid useragent 
		/// </summary>
		InvalidOrEmptyUserAgent = 411,

		/// <summary>
		/// %s has invalid format (reason) 
		/// </summary>
		InvalidFormat = 412,

		/// <summary>
		/// Invalid ImdbID 
		/// </summary>
		InvalidImdbId = 413,

		/// <summary>
		/// Unknown User Agent 
		/// </summary>
		//[StringValue("414 Unknown User Agent")]
		UnknownUserAgent = 414,

		/// <summary>
		/// Disabled user agent 
		/// </summary>
		DisabledUserAgent = 415,

		/// <summary>
		/// Internal subtitle validation failed 
		/// </summary>
		InternalSubtitleValidationFail = 416,

		#endregion Errors 4xx

		#region Server Error 5xx

		/// <summary>
		/// Service Unavailable 
		/// </summary>
		ServiceUnavailable = 503,

		#endregion Server Error 5xx
	}
}