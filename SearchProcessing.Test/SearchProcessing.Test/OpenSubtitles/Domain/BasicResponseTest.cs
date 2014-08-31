using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles.Domain;
using System.Collections.Generic;

namespace SearchProcessing.Test.OpenSubtitles.Domain
{
	/// <summary>
	/// Tests for the BasicResponse class.
	/// </summary>
	[TestClass]
	public class BasicResponseTest
	{
		#region Fields

		private Dictionary<ResponseStatusLookupId, string> _statusCodes;

		#endregion Fields

		[TestInitialize]
		public void TestInitialize()
		{
			_statusCodes = new Dictionary<ResponseStatusLookupId, string>
			{
				{ResponseStatusLookupId.Ok, "200 OK"},
				{ResponseStatusLookupId.OkPartial, "206 Partial content; message"},
				{ResponseStatusLookupId.HostMoved, "301 Moved (host)"},
				{ResponseStatusLookupId.Unauthorized, "401 Unauthorized"},
				{ResponseStatusLookupId.InvalidSubtitlesFormat, "402 Subtitles has invalid format"},
				{ResponseStatusLookupId.SubHashesNotMatch, "403 SubHashes (content and sent subhash) are not same!"},
				{ResponseStatusLookupId.InvalidSubtitleLanguage, "404 Subtitles has invalid language!"},
				{ResponseStatusLookupId.MissingParameters, "405 Not all mandatory parameters was specified"},
				{ResponseStatusLookupId.NoSession, "406 No session"},
				{ResponseStatusLookupId.DownloadLimitReached, "407 Download limit reached"},
				{ResponseStatusLookupId.InvalidParameters, "408 Invalid parameters"},
				{ResponseStatusLookupId.MethodNotFound, "409 Method not found"},
				{ResponseStatusLookupId.UnknownError, "410 Other or unknown error"},
				{ResponseStatusLookupId.InvalidOrEmptyUserAgent, "411 Empty or invalid useragent"},
				{ResponseStatusLookupId.InvalidFormat, "412 %s has invalid format (reason)"},
				{ResponseStatusLookupId.InvalidImdbId, "413 Invalid ImdbID"},
				{ResponseStatusLookupId.UnknownUserAgent, "414 Unknown User Agent"},
				{ResponseStatusLookupId.DisabledUserAgent, "415 Disabled user agent"},
				{ResponseStatusLookupId.InternalSubtitleValidationFail, "416 Internal subtitle validation failed"},
				{ResponseStatusLookupId.ServiceUnavailable, "503 Service Unavailable"},
			};
		}


		[TestMethod]
		public void GetResponseStatusTest()
		{
			BasicResponse basicResponse = new BasicResponse();

			foreach (KeyValuePair<ResponseStatusLookupId, string> statusCode in _statusCodes)
			{
				basicResponse.Status = statusCode.Value;

				ResponseStatusLookupId status = basicResponse.GetResponseStatus();

				Assert.AreEqual(
					statusCode.Key, 
					status, 
					string.Format("The actual status code: {0} did not match the expected status code: {1}", statusCode.Key.ToString(), status.ToString())
					);
			}
		}

		/// <summary>
		/// Tests the GetResponseStatus method when the Status parsing fails.
		/// </summary>
		[TestMethod]
		public void GetResponseStatusParseFailTest()
		{
			BasicResponse basicResponse = new BasicResponse
			{
				Status = "unParsable"
			};

			ResponseStatusLookupId status = basicResponse.GetResponseStatus();

			Assert.AreEqual(ResponseStatusLookupId.None, status, "The status should have been set to 'None'.");
		}
	}
}
