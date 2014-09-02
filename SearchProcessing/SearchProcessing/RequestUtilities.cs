using SearchProcessing.Constracts;
using System;
using System.Net;
using System.Runtime.Serialization.Json;

namespace SearchProcessing
{
	public static class RequestUtilities
	{
		#region Fields
		
		#endregion Fields
		
		public static TRootResult MakeRequest<TRootResult, TResult>(string requestUrl)
			where TRootResult : class, IRootObject<TResult>
			where TResult : class, IResult
		{
			try
			{
				HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response == null)
					{
						return default(TRootResult);
					}

					if (response.StatusCode != HttpStatusCode.OK)
					{
						throw new Exception(String.Format("Server Error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
					}

					DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TRootResult));
					
					object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());

					TRootResult jsonResponse = objResponse as TRootResult;

					return jsonResponse;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}
	}
}