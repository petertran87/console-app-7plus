using ConsoleApp7Plus.Services;
using ConsoleApp7Plus.Tests.Stubs;
using ConsoleApp7Plus.Utils;

namespace ConsoleApp7Plus.Tests.Abstraction
{
	public abstract class StubbingAbstract<TApi>
	{
		protected readonly HttpClient _httpClient;
		private readonly ILogger<TApi> _apiLogger;
		protected readonly LoggerFactory _loggerFactory;
		private readonly HandlerApiJsonResponse _apiResponseHandler;

		public StubbingAbstract(StubSampleApiHttpMessage messageHandler, double expectedTimeout = 3)
		{
			_loggerFactory = new();
			ILogger<HandlerApiJsonResponse> apiResponseHandlerLogger = _loggerFactory.CreateLogger<HandlerApiJsonResponse>();

			_apiLogger = _loggerFactory.CreateLogger<TApi>();
			_apiResponseHandler = new(apiResponseHandlerLogger);
			_httpClient = new(messageHandler) { Timeout = TimeSpan.FromSeconds(expectedTimeout), BaseAddress = new Uri("https://www.test.com") };
		}

		internal protected TApi ScaffoldApi(string endpoint = "mockApi")
		{
			return ApiFactory<TApi>.Init(_httpClient, _apiLogger, _apiResponseHandler, endpoint, "");
		}
	}
}