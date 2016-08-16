using NSwag;
using System.Net.Http;

namespace jab.Http
{
    public static class SwaggerServiceExtensions
    {
        public static HttpClient GetHttpClient(this SwaggerService service, string baseUrl)
        {
            return JabHttpClientFactory.GetClient(baseUrl);
        }
    }
}
