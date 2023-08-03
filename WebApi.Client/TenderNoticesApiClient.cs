using GocTenderNotices.Contracts.State;
using Microsoft.Extensions.DependencyModel;
using RestSharp;
using WebApi.Contracts.DTO;

namespace WebApi.Client
{
    /// <summary>
    /// [RestSharp RestClient Usage](https://restsharp.dev/usage.html)
    /// </summary>
    public class TenderNoticesApiClient : ITenderNoticesApiClient
    {
        private readonly TenderNoticeClientSettings _settings;
        private readonly RestClient _client;
        private const string Resources = "tendernotice";

        public TenderNoticesApiClient(TenderNoticeClientSettings settings)
        {
            _settings = settings;
            var options = new RestClientOptions(_settings.ApiUrl);
            _client = new RestClient(options)
            {
                // Authentication can be wired up here.
            };
        }

        public async Task<IEnumerable<TenderNoticeDto>> GetActiveAsync()
        {
            var response = await _client.GetJsonAsync<TenderNoticeDto[]>(Resources);
            return response!;
        }

        public async Task<TenderNoticeDto?> PostTenderNotice(TenderNoticeDto tenderNoticeDto)
        {
            var request = new RestRequest(Resources, Method.Post);
            request.AddJsonBody(tenderNoticeDto);

            var response = await _client.ExecuteAsync<TenderNoticeDto>(request);
            return response?.Data;
        }
    }
}