namespace ConsoleApp7Plus.Interfaces
{
	public interface ISampleAggregationGetUserName
	{
		public Task<string?> GetFullName(int id);
		public Task<List<string>> GetFullNames(int age);
	}
}