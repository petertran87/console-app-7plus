using Microsoft.Extensions.Logging;

using ConsoleApp7Plus.Abstraction;
using ConsoleApp7Plus.Enumerations;
using ConsoleApp7Plus.Models;
using ConsoleApp7Plus.Interfaces;

namespace ConsoleApp7Plus.Services
{
	public class SampleAggregation : Aggregation<UserAggregation>
	{
		protected readonly SampleApi _apiClient;
		protected readonly ILogger<SampleAggregation> _logger;

		public SampleAggregation(ILogger<SampleAggregation> logger, SampleApi apiClient) : base()
		{
			_apiClient = apiClient;
			_logger = logger;
		}

		public override async Task<List<UserAggregation>> AggregateDemographicData()
		{
			try
			{
				List<User> users = await _apiClient.GetRecords();

				return AggregateGenderData(users);
			}
			catch (Exception e)
			{
				_logger.LogCritical(e, "Encountered");
			}

			return new();
		}

		protected static List<UserAggregation> AggregateGenderData(List<User> input)
		{
			if (input.Count == 0)
			{
				return new();
			}

			Dictionary<int, UserAggregation> result = new();

			foreach (User user in input)
			{
				int age = user.Age;
				Gender gender = user.Gender;

				result.TryAdd(age, new UserAggregation { Age = age, Female = 0, Male = 0, Others = 0, Trans = 0 });

				switch (gender)
				{
					case Gender.F:
						result[age].Female++;
						break;
					case Gender.M:
						result[age].Male++;
						break;
					case Gender.T:
						result[age].Trans++;
						break;
					default:
						result[age].Others++;
						break;
				}
			}

			return result.Select(kv => kv.Value).OrderBy(v => v.Age).ToList();
		}
	}

	public class SampleAggregationUserName : SampleAggregation, ISampleAggregationGetUserName
	{
		public SampleAggregationUserName(ILogger<SampleAggregation> logger, SampleApi apiClient) : base(logger, apiClient)
		{ }

		public async Task<string?> GetFullName(int id)
		{
			try
			{
				User user = await _apiClient.GetRecordById(id);

				return GenerateFullName(user);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Encountered error while trying to retrieved user's full name based on ID");
			}

			return null;
		}

		public async Task<List<string>> GetFullNames(int age)
		{
			try
			{
				List<User> users = await _apiClient.GetRecords();

				if (users.Count == 0)
				{
					return new();
				}

				List<string> qualifiedFullNames = users.Where(u => u.Age == age).Select(GenerateFullName).ToList();

				return qualifiedFullNames;
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Encountered error while trying to retrieved users' full names based on age");
			}

			return new();
		}

		protected static string GenerateFullName(User user) => $"{user.FirstName.Trim()} {user.LastName.Trim()}";
	}
}

