using Microsoft.Extensions.Logging;

using ConsoleApp7Plus.Abstraction;
using ConsoleApp7Plus.Models;
using ConsoleApp7Plus.Exceptions;
using System.Net.Http.Headers;

namespace ConsoleApp7Plus.Services
{
    public class SampleApi : ApiInteraction<UserBasicData, User>
    {
        private readonly ILogger<SampleApi> _logger;
        private readonly HttpClient _httpClient;
        private readonly HandlerApiJsonResponse _requestHandler;

        public SampleApi(HttpClient httpClient, ILogger<SampleApi> logger, HandlerApiJsonResponse requestHandler, string endpoint, string accessToken = "") : base(endpoint)
        {
            _httpClient = httpClient;
            _logger = logger;
            _requestHandler = requestHandler;

            // In some protected route, ensure to use bearer token
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public override async Task<User> GetRecordById(int id)
        {
            //! This one can be enabled if the single route exists
            // Task<HttpResponseMessage> retrieveRecords()
            // {
            //     return _httpClient.GetAsync($"{_Endpoint}/{id}");
            // }

            // Since attempting to get access to /sampletest/:id always result in 403 Unauthorized response without an access token, we'll need to call GetRecords() method instead
            List<User> users = await GetRecords();

            foreach (User user in users)
            {
                if (id == user.Id)
                {
                    return user;
                }
            }

            throw new InvalidUserIdException(id);
        }

        public override async Task<List<User>> GetRecords()
        {
            // Define the method to retrieve the data
            Task<HttpResponseMessage> retrieveRecords()
            {
                return _httpClient.GetAsync(_Endpoint);
            }

            return await _requestHandler.ExecuteAsync<List<User>>(retrieveRecords, _Endpoint);
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

