using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Auth.PCL;

namespace Timesheet.Client.Shared.ResourceAccess
{
    public class ApiClient: IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly TimesheetApiConfig _config;

        public ApiClient(HttpClient httpClient, TimesheetApiConfig config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public IAccount TokenProvider { get; set; }

        public async Task<IEnumerable<TResource>> Fetch<TResource>(IApi<TResource> resource)
        {
            var fullUri = _config.BaseUrl + resource.RelativeUri;
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TokenProvider.Properties["access_token"]);
            var response = await _httpClient.SendAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<TResource>>(await response.Content.ReadAsStringAsync());
        }
    }
}