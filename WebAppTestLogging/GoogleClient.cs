using System.Net.Http;
using System.Threading.Tasks;

namespace WebAppTestLogging
{
    public class GoogleClient
    {
        private readonly HttpClient _httpClient;

        public GoogleClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<string> Hello() => _httpClient.GetStringAsync("");
    }
}