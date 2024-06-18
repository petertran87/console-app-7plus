using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ConsoleApp7Plus.Utils;
using ConsoleApp7Plus.Services;
using ConsoleApp7Plus.Models;

namespace ConsoleApp7Plus
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Please provide mandatory base URL to send request to:");
			string baseUrl = Console.ReadLine() ?? throw new InvalidExpressionException("Sorry... No base URL found...");

			if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var _))
			{
				throw new InvalidExpressionException($"Invalid base URL detected, received \"{baseUrl}\"");
			}

			Console.WriteLine("Please provide access token if any:");
			var accessToken = Console.ReadLine() ?? "";

			// Define dependency services to inject
			/** Register HttpClient with default settings:
			 *		1. Timeout is 5 seconds
			 *		2. Base URL per set in appsettings.json file
			 *		3. Retry policy to allow at most 3 retries with delay time up to 2^3 seconds
			 */
			ServiceProvider serviceProvider = new ServiceCollection().AddSingleton<HttpClient>((_) => new(new RetryPolicyDelegatingHandler()) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(5) })
				// Register logging servies
				.AddLogging(config => config.AddConsole())
				// Register services
				.AddTransient<HandlerApiJsonResponse>()
				.AddTransient<SampleAggregationUserName>()
				.AddTransient((provider) =>
				{
					HttpClient httpClient = provider.GetRequiredService<HttpClient>();
					ILogger<SampleApi> logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger<SampleApi>();
					HandlerApiJsonResponse handler = provider.GetRequiredService<HandlerApiJsonResponse>();

					return ServiceFactory<SampleApi>.Init(httpClient, logger, handler, "sampletest", accessToken);
				})
				.BuildServiceProvider();

			SampleAggregationUserName apiCall = serviceProvider.GetRequiredService<SampleAggregationUserName>();

			while (true)
			{
				Console.WriteLine();
				Console.WriteLine(@"Please enter your command:
	1 - Get a single user full name based on ID
	2 - Get a comma-separated value for users' full names based on age
	3 - Aggregate user geographic data for gender based on age
	9 - Quit
				");

				string choice = Console.ReadLine() ?? "";

				if (string.IsNullOrEmpty(choice) || !int.TryParse(choice, out int command))
				{
					Console.WriteLine($"Invalid command, received \"{choice}\", please try again...");
					continue;
				}

				if (command < 1 || (command > 3 && command != 9))
				{
					Console.WriteLine($"There is command code as specified, please use 1, 2 or 3, received {command}");
					continue;
				}

				switch (command)
				{
					case 1:
						{
							Console.WriteLine("Please enter your expected user ID:");
							string? userIdInput = Console.ReadLine();

							if (!int.TryParse(userIdInput, out int userId))
							{
								Console.WriteLine($"Invalid user ID input, expecting a number, received \"{userIdInput}\"");
								continue;
							}

							string? output = await apiCall.GetFullName(userId);

							if (!string.IsNullOrEmpty(output))
							{
								Console.WriteLine($"Output: {output}");
							}

							break;
						}
					case 2:
						{
							Console.WriteLine("Please enter your expected user age:");
							string? userAgeInput = Console.ReadLine();

							if (!int.TryParse(userAgeInput, out int age))
							{
								Console.WriteLine($"Invalid user age input, expecting a number, received \"{userAgeInput}\"");
								continue;
							}

							List<string> userFullNames = await apiCall.GetFullNames(age);

							if (userFullNames.Count == 0)
							{
								Console.WriteLine($"No people found with age {age}");
								continue;
							}

							Console.WriteLine($"Output for age {age}: {string.Join(", ", userFullNames)}");

							break;
						}
					case 3:
						{
							Console.WriteLine("Fetching demographic data for gender based on age...");

							List<UserAggregation> aggregations = await apiCall.AggregateDemographicData();

							if (aggregations.Count == 0)
							{
								Console.WriteLine("No demographic data is available due to malformed or no existing data");
								continue;
							}

							Console.WriteLine("Demographic data output:");

							foreach (UserAggregation aggregation in aggregations)
							{
								Console.WriteLine($" - Age : {aggregation.Age} Female: {aggregation.Female} Male: {aggregation.Male} Trans : {aggregation.Trans} Others : {aggregation.Others}");
							}

							break;
						}
					case 9:
						{
							Console.WriteLine($"RECEIVED comand: {command}");
							return;
						}
					// This one is just to prevent fall through
					default:
						break;
				}
			}
		}
	}
}


