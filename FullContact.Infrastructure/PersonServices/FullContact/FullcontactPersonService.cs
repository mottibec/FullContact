using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FullContact.Infrastructure
{
    public class FullcontactPersonService : BasePersonService, IPersonService
    {
        private readonly HttpClient _httpClient;

        public FullcontactPersonService(string name, string baseUrl, string apiKey = null)
            : base(name, baseUrl, apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public override async Task<IPersonResponse> Get(string identifier)
        {
            identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));

            var endpointUrl = "/v3/person.enrich";
            var values = new Dictionary<string, string>
            {
                { GetIdentifier(), identifier}
            };
            var json = JsonConvert.SerializeObject(values);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpointUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                //todo throw matching error
                throw new HttpRequestException("the person not found");
            }
            var personJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FullContactPersonResponse>(personJson);
        }

        public string GetIdentifier()
        {
            return "email";
        }
    }
}
