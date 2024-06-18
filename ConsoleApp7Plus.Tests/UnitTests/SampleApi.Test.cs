using ConsoleApp7Plus.Enumerations;
using ConsoleApp7Plus.Exceptions;
using ConsoleApp7Plus.Models;
using ConsoleApp7Plus.Services;
using ConsoleApp7Plus.Tests.Abstraction;
using ConsoleApp7Plus.Tests.Stubs;

namespace ConsoleApp7Plus.Tests.UnitTests
{
	public class SampleApiUnitTest
	{
		readonly List<User> stubData = new()
			{
				new() { Id = 1, Age = 18, FirstName = "Test", LastName = "User", Gender = Gender.M },
				new() { Id = 2, Age = 28, FirstName = "Test 1", LastName = "User 1", Gender = Gender.F }
			};

		[Fact]
		public async Task GetRecordById_ShouldReturnData()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, stubData);
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			User resData = await mockApi.GetRecordById(stubData.First().Id);

			stubData.First().Should().BeEquivalentTo(resData);
		}

		[Fact]
		public async Task GetRecordById_NonExistentUserId_ShouldThrowInvalidUserIdException()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, stubData);
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			await Assert.ThrowsAsync<InvalidUserIdException>(async () => await mockApi.GetRecordById(3));
		}

		[Fact]
		public async Task GetRecords_ShouldReturnData()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, stubData);
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			List<User> resData = await mockApi.GetRecords();

			stubData.Should().BeEquivalentTo(resData);
		}

		[Fact]
		public async Task GetRecords_ShouldReturnEmptyData()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, new());
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			Assert.Empty(await mockApi.GetRecords());
		}

		[Fact]
		public async Task GetRecordById_EndpointNotFound_ShouldThrowCustomRequestException()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.NotFound, stubData);
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			await Assert.ThrowsAsync<CustomRequestException>(async () => await mockApi.GetRecordById(1));
		}

		[Fact]
		public async Task GetRecordById_InternalServerError_ShouldThrowCustomRequestException()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.InternalServerError, stubData);
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			await Assert.ThrowsAsync<CustomRequestException>(async () => await mockApi.GetRecordById(1));
		}

		[Fact]
		public async Task GetRecordById_Unauthorized_ShouldThrowCustomRequestException()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.Unauthorized, stubData);
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			await Assert.ThrowsAsync<CustomRequestException>(async () => await mockApi.GetRecordById(1));
		}

		[Fact]
		public async Task GetRecordById_Forbidden_ShouldThrowCustomRequestException()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.Forbidden, stubData);
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			await Assert.ThrowsAsync<CustomRequestException>(async () => await mockApi.GetRecordById(1));
		}

		[Fact]
		public async Task GetRecordById_InvalidJson_ShouldThrowInvalidRequestOperationException()
		{
			StubSampleApiHttpMessage handler = new(HttpStatusCode.OK, null);
			StubbingApi stubbing = new(handler);

			SampleApi mockApi = stubbing.ScaffoldApi();

			await Assert.ThrowsAsync<InvalidRequestOperationException<List<User>>>(async () => await mockApi.GetRecordById(1));
		}
	}
}