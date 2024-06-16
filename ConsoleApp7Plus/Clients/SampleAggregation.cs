using ConsoleApp7Plus.Abstraction;
using ConsoleApp7Plus.Models;
namespace ConsoleApp7Plus.Clients
{
	public class SampleAggregation : Aggregation<UserAggregation>
	{
		public SampleAggregation(string endpoint) : base(endpoint) { }

		public override Task<List<UserAggregation>> AggregateDemographicData()
		{
			throw new NotImplementedException();
		}
	}
}

