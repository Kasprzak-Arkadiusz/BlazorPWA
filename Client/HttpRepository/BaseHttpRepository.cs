using Application.Common.Responses;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository
{
    public abstract class BaseHttpRepository
    {
        protected readonly JsonSerializerOptions Options;
        protected readonly HttpClient HttpClient;
        private readonly string _requestUri;

        protected BaseHttpRepository(IHttpClientFactory httpClientFactory, string requestUri)
        {
            _requestUri = requestUri;
            Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            HttpClient = httpClientFactory.CreateClient("WebAPI");
        }

        protected async Task<CreateResponse> CreateAsync<T>(T elementToAdd)
        {
            var json = JsonSerializer.Serialize(elementToAdd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(_requestUri, content);

            var createResponse = await response.Content.ReadFromJsonAsync<CreateResponse>();

            return createResponse;
        }

        protected async Task<bool> UpdateAsync<T>(T elementToUpdate)
        {
            var json = JsonSerializer.Serialize(elementToUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync(_requestUri, content);

            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync($"{_requestUri}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}