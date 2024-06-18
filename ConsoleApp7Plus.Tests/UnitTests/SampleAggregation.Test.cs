using ConsoleApp7Plus.Enumerations;
using ConsoleApp7Plus.Exceptions;
using ConsoleApp7Plus.Models;
using ConsoleApp7Plus.Services;
using ConsoleApp7Plus.Tests.Stubs;

namespace ConsoleApp7Plus.Tests.UnitTests
{
	public class SampleAggregationUnitTest
	{
		readonly List<User> stubData = new()
			{
				new() { Id = 1, Age = 18, FirstName = "Test", LastName = "User 1", Gender = Gender.M },
				new() { Id = 2, Age = 28, FirstName = "Test", LastName = "User 2", Gender = Gender.F },
				new() { Id = 3, Age = 65, FirstName = "Test", LastName = "User 3", Gender = Gender.T },
				new() { Id = 4, Age = 20, FirstName = "Test", LastName = "User 4", Gender = Gender.Y }
			};
		readonly List<UserAggregation> stubOutput = new()
		{
			new() { Age = 18, Female = 0, Male = 1, Trans = 0, Others = 0 },
			new() { Age = 20, Female = 0, Male = 0, Trans = 0, Others = 1 },
			new() { Age = 28, Female = 1, Male = 0, Trans = 0, Others = 0 },
			new() { Age = 65, Female = 0, Male = 0, Trans = 1, Others = 0 },
		};

		[Fact]
		public async Task AggregateDemographicData_ShouldReturnData()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, stubData);
			StubbingAggregation<SampleAggregation> stubbing = new(handler);

			SampleAggregation mockAggregation = stubbing.ScaffoldConsumer();
			List<UserAggregation> aggregation = await mockAggregation.AggregateDemographicData();

			stubOutput.Should().BeEquivalentTo(aggregation);
		}

		[Fact]
		public async Task AggregateDemographicData_EndpointNotFound_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.NotFound, stubData);
			StubbingAggregation<SampleAggregation> stubbing = new(handler);

			SampleAggregation mockAggregation = stubbing.ScaffoldConsumer();
			List<UserAggregation> aggregation = await mockAggregation.AggregateDemographicData();

			Assert.Empty(aggregation);
		}

		[Fact]
		public async Task AggregateDemographicData_InternalServerError_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.InternalServerError, stubData);
			StubbingAggregation<SampleAggregation> stubbing = new(handler);

			SampleAggregation mockAggregation = stubbing.ScaffoldConsumer();
			List<UserAggregation> aggregation = await mockAggregation.AggregateDemographicData();

			Assert.Empty(aggregation);
		}

		[Fact]
		public async Task AggregateDemographicData_Unauthorized_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.Unauthorized, stubData);
			StubbingAggregation<SampleAggregation> stubbing = new(handler);

			SampleAggregation mockAggregation = stubbing.ScaffoldConsumer();
			List<UserAggregation> aggregation = await mockAggregation.AggregateDemographicData();

			Assert.Empty(aggregation);
		}

		[Fact]
		public async Task AggregateDemographicData_Forbidden_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.Forbidden, stubData);
			StubbingAggregation<SampleAggregation> stubbing = new(handler);

			SampleAggregation mockAggregation = stubbing.ScaffoldConsumer();
			List<UserAggregation> aggregation = await mockAggregation.AggregateDemographicData();

			Assert.Empty(aggregation);
		}

		[Fact]
		public async Task AggregateDemographicData_InvalidJson_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, null);
			StubbingAggregation<SampleAggregation> stubbing = new(handler);

			SampleAggregation mockAggregation = stubbing.ScaffoldConsumer();
			List<UserAggregation> aggregation = await mockAggregation.AggregateDemographicData();

			Assert.Empty(aggregation);
		}
	}

	public class SampleAggregationUserNameUnitTest
	{
		readonly List<User> stubData = new()
			{
				new() { Id = 1, Age = 18, FirstName = "Test", LastName = "User 1", Gender = Gender.M },
				new() { Id = 2, Age = 28, FirstName = "Test", LastName = "User 2", Gender = Gender.F },
				new() { Id = 3, Age = 65, FirstName = "Test", LastName = "User 3", Gender = Gender.T },
				new() { Id = 4, Age = 20, FirstName = "Test", LastName = "User 4", Gender = Gender.Y },
				new() { Id = 5, Age = 28, FirstName = "Test", LastName = "User 5", Gender = Gender.M }
			};

		[Fact]
		public async Task GetFullName_ShouldReturnData()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			string? aggregation = await mockAggregation.GetFullName(2);

			Assert.Equal("Test User 2", aggregation);
		}

		[Fact]
		public async Task GetFullName_EndpointNotFound_ShouldReturnNull()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.NotFound, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			string? aggregation = await mockAggregation.GetFullName(2);

			Assert.Null(aggregation);
		}

		[Fact]
		public async Task GetFullName_InternalServerError_ShouldReturnNull()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.InternalServerError, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			string? aggregation = await mockAggregation.GetFullName(2);

			Assert.Null(aggregation);
		}

		[Fact]
		public async Task GetFullName_Unauthorized_ShouldReturnNull()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.Unauthorized, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			string? aggregation = await mockAggregation.GetFullName(2);

			Assert.Null(aggregation);
		}

		[Fact]
		public async Task GetFullName_Forbidden_ShouldReturnNull()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.Forbidden, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			string? aggregation = await mockAggregation.GetFullName(2);

			Assert.Null(aggregation);
		}

		[Fact]
		public async Task GetFullName_InvalidJson_ShouldReturnNull()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, null);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			string? aggregation = await mockAggregation.GetFullName(2);

			Assert.Null(aggregation);
		}

		[Fact]
		public async Task GetFullNames_ShouldReturnData()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			List<string> aggregation = await mockAggregation.GetFullNames(28);

			Assert.Equal(new() { "Test User 2", "Test User 5" }, aggregation);
		}

		[Fact]
		public async Task GetFullNames_EndpointNotFound_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.NotFound, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			List<string> aggregation = await mockAggregation.GetFullNames(2);

			Assert.Empty(aggregation);
		}

		[Fact]
		public async Task GetFullNames_InternalServerError_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.InternalServerError, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			List<string> aggregation = await mockAggregation.GetFullNames(2);

			Assert.Empty(aggregation);
		}

		[Fact]
		public async Task GetFullNames_Unauthorized_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.Unauthorized, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			List<string> aggregation = await mockAggregation.GetFullNames(2);

			Assert.Empty(aggregation);
		}

		[Fact]
		public async Task GetFullNames_Forbidden_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.Forbidden, stubData);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			List<string> aggregation = await mockAggregation.GetFullNames(2);

			Assert.Empty(aggregation);
		}

		[Fact]
		public async Task GetFullNames_InvalidJson_ShouldReturnEmpty()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, null);
			StubbingAggregation<SampleAggregationUserName> stubbing = new(handler);

			SampleAggregationUserName mockAggregation = stubbing.ScaffoldConsumer();
			List<string> aggregation = await mockAggregation.GetFullNames(2);

			Assert.Empty(aggregation);
		}
	}
}