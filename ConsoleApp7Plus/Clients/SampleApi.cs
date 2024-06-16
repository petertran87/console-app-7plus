using ConsoleApp7Plus.Abstraction;
using ConsoleApp7Plus.Models;

namespace ConsoleApp7Plus.Clients
{
	public class SampleApi : ApiInteraction<UserBasicData, User>
	{
		public SampleApi(string endpoint) : base(endpoint) { }

		public override Task<User> GetRecordById(int id)
		{
            throw new NotImplementedException();
        }

		public override Task<Dictionary<int, User>> GetRecords()
		{
            throw new NotImplementedException();
        }

		public override Task<int> CreateRecord(UserBasicData data)
		{
            throw new NotImplementedException();
        }

        public override Task UpdateRecord(User data)
        {
            throw new NotImplementedException();
        }
    }
}

