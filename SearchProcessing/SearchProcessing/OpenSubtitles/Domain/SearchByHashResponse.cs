using System;
using System.Collections;

using CookComputing.XmlRpc;
using System.Linq;

namespace SearchProcessing.OpenSubtitles.Domain
{
	public class SearchByHashResponse : BasicResponse
	{
		#region Fields

		/*
		 NonSerialized attributes are necessary so that XmlRpc doesn't try to map these value to values that are not returned
		 */

		[NonSerialized]
		public const string _movieHash = "MovieHash";

		[NonSerialized]
		public const string _movieImdbId = "MovieImdbID";

		[NonSerialized]
		public const string _movieName = "MovieName";

		[NonSerialized]
		public const string _movieYear = "MovieYear";

		[NonSerialized]
		public const string _movieKind = "MovieKind";

		[NonSerialized]
		public const string _seriesSeason = "SeriesSeason";

		[NonSerialized]
		public const string _seriesEpisode = "SeriesEpisode";

		[NonSerialized]
		public const string _seenCount = "SeenCount";

		[NonSerialized]
		public const string _subCount = "SubCount";

		#endregion Fields

		[XmlRpcMember("data")]
		public XmlRpcStruct MediaData
		{
			get;
			private set;
		}

		#region Properties Of Hash

		/// <summary>
		/// Gets the dictionary value of the XmlRpcStruct based on the key input.
		/// </summary>
		/// <param name="key">One of the known dictionary keys; they are constants in this class.</param>
		/// <returns>The value of the key if there is a match; otherwise an empty string.</returns>
		public string GetMediaDataField(string key)
		{
			string returnedValue = string.Empty;

			IEnumerator enumerator = MediaData.Values.GetEnumerator();
			enumerator.MoveNext();
			XmlRpcStruct firstMatch = enumerator.Current as XmlRpcStruct;

			if (MediaData.Count == 1 && firstMatch != null)
			{
				returnedValue = firstMatch[key] as string;
			}

			return returnedValue;
		}

		#endregion Properties Of Hash
	}
}