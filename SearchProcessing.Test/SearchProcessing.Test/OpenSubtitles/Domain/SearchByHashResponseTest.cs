using CookComputing.XmlRpc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles.Domain;

namespace SearchProcessing.Test.OpenSubtitles.Domain
{
	[TestClass]
	public class SearchByHashResponseTest
	{
		#region Fields

		private const string _hash = "0123012301230123";

		#endregion Fields

		/// <summary>
		/// Tests the GetMediaDateField method.
		/// </summary>
		[TestMethod]
		public void GetMediaDateFieldMethod()
		{
			XmlRpcStruct mediaData = new XmlRpcStruct();

			SearchByHashResponse searchByHashResponse = new SearchByHashResponse
			{
				MediaData = mediaData
			};

			XmlRpcStruct match = new XmlRpcStruct
			{
				{ "MovieHash", _hash },
				{ "MovieImdbID", "8008" },
				{ "MovieName", "TMNT" }
			};

			mediaData.Add(_hash, match);

			string actual = searchByHashResponse.GetMediaDataField("MovieHash");

			Assert.AreEqual(_hash, actual, "The returned value did not match according to the key passed in.");
		}

		/// <summary>
		/// Tests the GetMediaDateField method when multiple key value pairs have been added to MediaData (hash too vague most likely).
		/// </summary>
		[TestMethod]
		public void GetMediaDateFieldMultipleEntriesMethod()
		{
			XmlRpcStruct mediaData = new XmlRpcStruct();

			SearchByHashResponse searchByHashResponse = new SearchByHashResponse
			{
				MediaData = mediaData
			};

			XmlRpcStruct match = new XmlRpcStruct
			{
				{ "MovieHash", _hash },
				{ "MovieImdbID", "8008" },
				{ "MovieName", "TMNT" }
			};

			mediaData.Add(_hash, match);
			mediaData.Add("some other key", match);

			string actual = searchByHashResponse.GetMediaDataField("MovieHash");

			Assert.AreEqual(string.Empty, actual, "The returned value should have been an empty string since there was more than 1 key value pair in the MediaData object.");
		}

		/// <summary>
		/// Tests the GetMediaDateField method when the MediaData does not contain an <see cref="XmlRpcStruct"/> object, but an empty object array; 
		/// this occurs when a searched hash does not return with any results.
		/// </summary>
		[TestMethod]
		public void GetMediaDateFieldNoMatchMethod()
		{
			XmlRpcStruct mediaData = new XmlRpcStruct();

			SearchByHashResponse searchByHashResponse = new SearchByHashResponse
			{
				MediaData = mediaData
			};

			object[] emptyArray = new object[0];

			mediaData.Add(_hash, emptyArray);

			string actual = searchByHashResponse.GetMediaDataField("MovieHash");

			Assert.AreEqual(string.Empty, actual, "The returned value should have been an empty string.");
		}

		/// <summary>
		/// Tests the GetMediaDateField method when the MediaData does not contain an <see cref="XmlRpcStruct"/> object; 
		/// this action should not occur since even a no match would result in an empty object array being preset.
		/// </summary>
		[TestMethod]
		public void GetMediaDateFieldEmptyMediaDataMethod()
		{
			XmlRpcStruct mediaData = new XmlRpcStruct();

			SearchByHashResponse searchByHashResponse = new SearchByHashResponse
			{
				MediaData = mediaData
			};
			
			string actual = searchByHashResponse.GetMediaDataField("MovieHash");

			Assert.AreEqual(string.Empty, actual, "The returned value should have been an empty string.");
		}
	}
}
