using ConsoleApp7Plus.Abstraction;
using Microsoft.Extensions.Logging;

namespace ConsoleApp7Plus.Utils
{
	public static class ServiceFactory<TService>
	{
		public static TService Init(HttpClient httpClient, ILogger<TService> logger, ResponseHandler handler, string endpoint, string accessToken)
		{
			var constructor = typeof(TService).GetConstructor(new[] { typeof(HttpClient), typeof(ILogger<TService>), typeof(ResponseHandler), typeof(string), typeof(string) }) ?? throw new InvalidOperationException($"Sorry... {typeof(TService)} constructor cannot be found...");

			return (TService)constructor.Invoke(new object[] { httpClient, logger, handler, endpoint, accessToken });
		}
	}
}