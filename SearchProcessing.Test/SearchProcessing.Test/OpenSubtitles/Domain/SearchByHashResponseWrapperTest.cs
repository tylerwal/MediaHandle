using System;
using System.Collections.Generic;
using System.Security.Policy;

using CookComputing.XmlRpc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchProcessing.OpenSubtitles.Domain;

namespace SearchProcessing.Test.OpenSubtitles.Domain
{
	/// <summary>
	/// Test class for <see cref="SearchByHashResponseWrapper"/> class.
	/// </summary>
	[TestClass]
	public class SearchByHashResponseWrapperTest
	{
		#region Fields
		
		private const string _hash = "0123012301230123";
		private const string _imdb = "8008";
		private const string _year = "2014";
		private const string _name = "TMNT";
		private const string _kind = "movie";
		private const string _season = "5";
		private const string _episode = "3";
		private const string _subCount = "269";

		private const string _didNotMatchTemplate = "The {0} values did not match.";

		private static List<HashInfo> _hashInfos;

		#endregion Fields

		/// <summary>
		/// Initializes the test class.
		/// </summary>
		/// <param name="context">The context.</param>
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			_hashInfos = new List<HashInfo>
			{
				new HashInfo(sr => sr.Hash, SearchByHashResponse._movieHash, "0123012301230123"),
				new HashInfo(sr => sr.ImdbId, SearchByHashResponse._movieImdbId, "8008"),
				new HashInfo(sr => sr.Year, SearchByHashResponse._movieYear, "2014", 2014),
				new HashInfo(sr => sr.Name, SearchByHashResponse._movieName, "TMNT"),
				new HashInfo(sr => sr.KindLookupId, SearchByHashResponse._movieKind, "movie", (int)OpenSubtitlesKind.Movie),
				new HashInfo(sr => sr.Season, SearchByHashResponse._seriesSeason, "5", 5),
				new HashInfo(sr => sr.Episode, SearchByHashResponse._seriesEpisode, "3", 3),
				new HashInfo(sr => sr.SubtitlesCount, SearchByHashResponse._subCount, "269", 269)
			};
		}

		/// <summary>
		/// Tests the transformation of the <see cref="SearchByHashResponse"/> object to <see cref="SearchByHashResponseWrapper"/> that takes place in the constuctor.
		/// </summary>
		[TestMethod]
		public void BasicConstructorTest()
		{
			SearchByHashResponseWrapper wrapper = new SearchByHashResponseWrapper(GetInitializedSearchByHashResponse());

			foreach (HashInfo hashInfo in _hashInfos)
			{
				object expected = hashInfo.TestValue;

				if (hashInfo.WrappedValue != null)
				{
					expected = hashInfo.WrappedValue;
				}

				Assert.AreEqual(expected, hashInfo.PropertyFunc(wrapper), _didNotMatchTemplate, hashInfo.ResponseConstant);
			}
		}

		/// <summary>
		/// Tests the ParseNumberField method.
		/// </summary>
		[TestMethod]
		public void ParseNumberFieldTest()
		{
			SearchByHashResponse searchByHashResponse = GetInitializedSearchByHashResponse();

			XmlRpcStruct match = searchByHashResponse.MediaData[_hash] as XmlRpcStruct;

			match[SearchByHashResponse._movieYear] = "1997";

			SearchByHashResponseWrapper wrapper = new SearchByHashResponseWrapper(searchByHashResponse);

			Assert.AreEqual(1997, wrapper.Year, "The number (Year) was not parsed correctly.");
		}

		/// <summary>
		/// Tests the ParseNumberField method when parsing fails and a null value is returned.
		/// </summary>
		[TestMethod]
		public void ParseNumberFieldNullReturnTest()
		{
			SearchByHashResponse searchByHashResponse = GetInitializedSearchByHashResponse();

			XmlRpcStruct match = searchByHashResponse.MediaData[_hash] as XmlRpcStruct;

			match[SearchByHashResponse._movieYear] = "NAN";

			SearchByHashResponseWrapper wrapper = new SearchByHashResponseWrapper(searchByHashResponse);

			Assert.AreEqual(null, wrapper.Year, "The number (Year) should have been null since parsing should have failed.");
		}

		/// <summary>
		/// Returns a <see cref="SearchByHashResponse"/> object that has been initialized.
		/// </summary>
		/// <returns></returns>
		private SearchByHashResponse GetInitializedSearchByHashResponse()
		{
			XmlRpcStruct mediaData = new XmlRpcStruct();

			SearchByHashResponse searchByHashResponse = new SearchByHashResponse
			{
				MediaData = mediaData
			};

			XmlRpcStruct match = new XmlRpcStruct
			{
				{ SearchByHashResponse._movieHash, _hash },
				{ SearchByHashResponse._movieImdbId, _imdb },
				{ SearchByHashResponse._movieYear, _year },
				{ SearchByHashResponse._movieName, _name },
				{ SearchByHashResponse._movieKind, _kind },
				{ SearchByHashResponse._seriesSeason, _season },
				{ SearchByHashResponse._seriesEpisode, _episode },
				{ SearchByHashResponse._subCount, _subCount }
			};

			mediaData.Add(_hash, match);

			return searchByHashResponse;
		}

		private class HashInfo
		{
			#region Properties

			public Func<SearchByHashResponseWrapper, object> PropertyFunc
			{
				get;
				private set;
			}

			public string ResponseConstant
			{
				get;
				private set;
			}

			public string TestValue
			{
				get;
				private set;
			}

			public object WrappedValue
			{
				get;
				private set;
			}

			#endregion Properties

			#region Constructors
			
			public HashInfo(Func<SearchByHashResponseWrapper, object> propertyFunc, string responseConstant, string testValue)
			{
				PropertyFunc = propertyFunc;
				ResponseConstant = responseConstant;
				TestValue = testValue;
			}

			public HashInfo(Func<SearchByHashResponseWrapper, object> propertyFunc, string responseConstant, string testValue, object wrappedValue) 
				: this(propertyFunc, responseConstant, testValue)
			{
				WrappedValue = wrappedValue;
			}

			#endregion Constructors
		}
	}
}
