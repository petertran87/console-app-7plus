using ConsoleApp7Plus.Utils;

namespace ConsoleApp7Plus.Tests.Abstraction
{
	public abstract class StubHttpMessageHandler<T> : HttpMessageHandler
	{
		protected readonly HttpStatusCode _statusCode;
		protected readonly string _response;
		protected readonly bool _timeOutTest;

		public StubHttpMessageHandler(HttpStatusCode statusCode, T? stubData) : base()
		{
			_response = stubData != null ? CommonUtils.StringifyJson(stubData) : "{ gender: 16 }";
			_statusCode = statusCode;
		}

		protected abstract Task<HttpResponseMessage> SendAsyncWithTimeOut(HttpRequestMessage request, CancellationToken cancellationToken);
	}
}