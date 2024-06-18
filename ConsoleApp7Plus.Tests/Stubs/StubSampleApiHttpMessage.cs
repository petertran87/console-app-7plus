using ConsoleApp7Plus.Models;
using ConsoleApp7Plus.Services;
using ConsoleApp7Plus.Tests.Abstraction;
using ConsoleApp7Plus.Utils;

namespace ConsoleApp7Plus.Tests.Stubs
{
	public class StubbingApi : StubbingAbstract<SampleApi>
	{
		public StubbingApi(StubSampleApiHttpMessage messageHandler, double expectedTimeout = 3) : base(messageHandler, expectedTimeout)
		{ }
	}

	public class StubbingAggregation<T> : StubbingApi
	{
		private readonly ILogger<T> _consumerLogger;

		public StubbingAggregation(StubSampleApiHttpMessage messageHandler, double expectedTimeout = 3) : base(messageHandler, expectedTimeout)
		{
			_consumerLogger = _loggerFactory.CreateLogger<T>();
		}

		internal protected T ScaffoldConsumer()
		{
			SampleApi apiClient = ScaffoldApi();
			return ServiceFactory<SampleApi, T>.Init(_consumerLogger, apiClient);
		}
	}

	public class StubSampleApiHttpMessage : StubHttpMessageHandler<List<User>>
	{
		public StubSampleApiHttpMessage(HttpStatusCode statusCode, List<User>? stubData) : base(statusCode, stubData)
		{ }

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			HttpResponseMessage res = new() { StatusCode = _statusCode, Content = new StringContent(_response) };
			return await Task.FromResult(res);
		}

		protected override async Task<HttpResponseMessage> SendAsyncWithTimeOut(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			await Task.Delay(Timeout.Infinite, cancellationToken);
			throw new TaskCanceledException();
		}
	}
}
