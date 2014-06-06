using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHandleUtilities
{
	public static class StringExtensions
	{
		/// <summary>
		/// Extension method for strings that allow case insensitive 'contains'.
		/// code found @ http://stackoverflow.com/questions/444798/case-insensitive-containsstring
		/// </summary>
		/// <param name="source"></param>
		/// <param name="textToLookFor"></param>
		/// <param name="stringComparisonType"></param>
		/// <returns></returns>
		public static bool Contains(this string source, string textToLookFor, StringComparison stringComparisonType)
		{
			return source.IndexOf(textToLookFor, stringComparisonType) >= 0;
		}
	}
}
