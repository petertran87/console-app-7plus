using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System.Net;

namespace ConsoleApp7Plus.Utils
{
	public class RetryPolicyDelegatingHandler : DelegatingHandler
	{
		public RetryPolicyDelegatingHandler() : base(new HttpClientHandler())
		{
			InnerHandler = new HttpClientHandler();
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken cancellationToken)
		{
			AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions.HandleTransientHttpError().OrResult(res => res.StatusCode == HttpStatusCode.TooManyRequests).WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

			return await retryPolicy.ExecuteAsync(() => base.SendAsync(req, cancellationToken));
		}
	}
}