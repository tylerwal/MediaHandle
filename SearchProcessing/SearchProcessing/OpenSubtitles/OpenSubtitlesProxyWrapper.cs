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

		private IOpenSubtitlesProxy _proxy;

		private LogInResponse _logInResponse;

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
			_proxy.LogOut(_logInResponse.Token);
		}

		#endregion IDisposable Members

		#region Methods



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

		#endregion Helper Methods
	}
}
