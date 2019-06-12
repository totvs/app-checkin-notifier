using System.Net.Http;

namespace Microsoft.Extensions.Http
{
    public interface IConfigureHttpClient
    {
        void Configure(HttpClient httpClient);
    }
}
