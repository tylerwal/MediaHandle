using CookComputing.XmlRpc;
using MediaHandleUtilities.Configuration;
using SearchProcessing.OpenSubtitles.Domain;
using System;

namespace SearchProcessing.OpenSubtitles
{
	/// <summary>
	/// Wrapper class after the IOpenSubtitlesProxy.  Implements IDisposable to logout the user after completing search request.
	/// </summary>
	public class OpenSubtitlesProxyWrapper : IDisposable
	{
		#region Fields

		private string _username;
		private string _password;
		private string _language;
		private string _userAgent;

		private readonly IOpenSubtitlesProxy _proxy;

		private readonly LogInResponse _logInResponse;

		#endregion Fields

		#region Constructor

		public OpenSubtitlesProxyWrapper()
		{
			InitializeConfiguration();

			_proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();

			_logInResponse = _proxy.LogIn(_username, _password, _language, _userAgent);
		}

		#endregion Constructor

		#region IDisposable Members

		public void Dispose()
		{
			LogOut();
		}

		#endregion IDisposable Members

		#region Methods

		public ResponseStatusLookupId GetSessionStatus()
		{
			BasicResponse response = _proxy.SessionCheck(_logInResponse.Token);

			return response.GetResponseStatus();
		}

		#endregion Methods

		#region Helper Methods

		/// <summary>
		/// Sets local fields to those from the app.config (configuration file).
		/// </summary>
		private void InitializeConfiguration()
		{
			ConfigurationSettings.Initialize();

			_username = ConfigurationSettings.OpenSubtitles.Username;
			_password = ConfigurationSettings.OpenSubtitles.Password;
			_language = ConfigurationSettings.OpenSubtitles.Language;
			_userAgent = ConfigurationSettings.OpenSubtitles.UserAgent;
		}

		private bool LogOut()
		{
			ResponseStatusLookupId status = _proxy.LogOut(_logInResponse.Token).GetResponseStatus();

			return status == ResponseStatusLookupId.Ok;
		}

		#endregion Helper Methods
	}
}
