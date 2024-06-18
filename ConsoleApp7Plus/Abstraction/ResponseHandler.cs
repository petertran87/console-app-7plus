using ConsoleApp7Plus.Exceptions;

namespace ConsoleApp7Plus.Abstraction
{
	/// <summary>Pseudo API call to retrieve the response</summary>
	public delegate Task<HttpResponseMessage> ApiCall();

	/// <summary>Abstract class to handle all response data</summary>
	public abstract class ResponseHandler
	{
		/// <summary>Execute an asynchronous call to retrive the data specified</summary>
		/// <typeparam name="T">Return type of the async call</typeparam>
		/// <param name="func">Pseudo API call to retrieve the response to process the data</param>
		/// <param name="endpoint">Name of the endpoint for logging purpose</param>
		/// <returns>Expected data or exception thrown</returns>
		/// <exception cref="CustomRequestException"/>
		/// <exception cref="InvalidUserIdException"/>
		/// <exception cref="InvalidRequestOperationException{T}"/>
		public abstract Task<T> ExecuteAsync<T>(ApiCall func, string endpoint);
	}
}