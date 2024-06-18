namespace ConsoleApp7Plus.Abstraction
{
	/// <summary>An abstract class to define all available aggregation methods in an API interaction class</summary>
	public abstract class Aggregation<T>
	{
		/// <summary>Base constructor for abstract class to scaffold an API aggregation class</summary>
		public Aggregation()
		{ }

		/// <summary>Aggregate data to get demographic data groups</summary>
		/// <typeparam name="T">Data type used for demographic data</typeparam>
		/// <returns>List of demographic data groups, return empty if there is no available data</returns>
		public abstract Task<List<T>> AggregateDemographicData();
	}
}

