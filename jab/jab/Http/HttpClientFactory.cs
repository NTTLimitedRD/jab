using System.Net.Http;

namespace jab.Http
{
    public static class HttpClientFactory
    {
        public static HttpClient GetClient(string baseUrl)
        {
            return new HttpClient() { BaseAddress = new System.Uri(baseUrl) };
        }
    }
}
