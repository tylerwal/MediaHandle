using System;

namespace Utilities
{
    public static class StringExtensions
    {
	    /// <summary>
	    /// Extension method for strings that allow case insensitive 'contains'.
	    /// code found @ http://stackoverflow.com/questions/444798/case-insensitive-containsstring
	    /// </summary>
	    /// <param name="source"></param>
	    /// <param name="toCheck"></param>
	    /// <param name="comp"></param>
	    /// <returns></returns>
	    public static bool Contains(this string source, string toCheck, StringComparison comp)
	    {
		    return source.IndexOf(toCheck, comp) >= 0;
	    }
    }
}
