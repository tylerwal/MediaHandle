using System;
using System.Collections.Generic;

namespace SearchProcessing.Constracts
{
	public interface IRootObject<TResults>
		where TResults : IResult
	{
		List<TResults> Results
		{
			get;
			set;
		}

		int TotalResults
		{
			get;
			set;
		}
	}
}