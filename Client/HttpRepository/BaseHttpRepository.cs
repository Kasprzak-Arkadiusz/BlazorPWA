using System.Net.Http;
using System.Text.Json;

namespace Client.HttpRepository
{
    public abstract class BaseHttpRepository
    {
        protected readonly JsonSerializerOptions Options;
        protected readonly HttpClient HttpClient;

        protected BaseHttpRepository(IHttpClientFactory httpClientFactory)
        {
            Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            HttpClient = httpClientFactory.CreateClient("WebAPI");
        }
    }
}