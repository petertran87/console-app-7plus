using ConsoleApp7Plus.Abstraction;
using ConsoleApp7Plus.Services;
using Microsoft.Extensions.Logging;

namespace ConsoleApp7Plus.Utils
{
	public static class ApiFactory<TApi>
	{
		public static TApi Init(HttpClient httpClient, ILogger<TApi> logger, HandlerApiJsonResponse handler, string endpoint, string? accessToken = null)
		{
			var constructor = typeof(TApi).GetConstructor(new[] { typeof(HttpClient), typeof(ILogger<TApi>), typeof(HandlerApiJsonResponse), typeof(string), typeof(string) }) ?? throw new InvalidOperationException($"Sorry... {typeof(TApi)} constructor cannot be found...");

			return (TApi)constructor.Invoke(new object[] { httpClient, logger, handler, endpoint, accessToken ?? "" });
		}
	}

	public static class ServiceFactory<TApi, TService>
	{
		public static TService Init(ILogger<TService> logger, TApi apiClient)
		{
			var constructor = typeof(TService).GetConstructor(new[] { typeof(ILogger<TService>), typeof(TApi) }) ?? throw new InvalidOperationException($"Sorry... {typeof(TService)} constructor cannot be found...");

			return (TService)constructor.Invoke(new object[] { logger, apiClient });
		}
	}
}