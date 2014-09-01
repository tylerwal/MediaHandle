namespace SearchProcessing.Constracts
{
	public interface IRequest
	{
		string CreateSearchQuery(string queryString);
	}
}