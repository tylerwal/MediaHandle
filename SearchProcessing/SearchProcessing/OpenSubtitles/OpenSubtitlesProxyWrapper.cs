using CookComputing.XmlRpc;
using SearchProcessing.OpenSubtitles.Domain;
using SearchProcessing.Utilities;
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

		#endregion Fields

		#region Constructor

		public OpenSubtitlesProxyWrapper()
		{
			InitializeConfiguration();

			IOpenSubtitlesProxy proxy = XmlRpcProxyGen.Create<IOpenSubtitlesProxy>();

			LogInResponse logInResponse = proxy.LogIn(_username, _password, _language, _userAgent);
		}

		#endregion Constructor

		#region IDisposable Members

		public void Dispose()
		{
			throw new NotImplementedException();
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
			Configuration.Initialize();

			_username = Configuration.OpenSubtitles.Username;

			_password = Configuration.OpenSubtitles.Password;

			_language = Configuration.OpenSubtitles.Language;

			_userAgent = Configuration.OpenSubtitles.UserAgent;
		}

		#endregion Helper Methods
	}
}
