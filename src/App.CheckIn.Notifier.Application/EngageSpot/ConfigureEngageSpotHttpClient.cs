using System;
using System.Net.Http;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;

namespace App.CheckIn.Notifier.Application.EngageSpot
{
    public class ConfigureEngageSpotHttpClient : IConfigureHttpClient
    {
        private readonly EngageSpotOptions _options;

        public ConfigureEngageSpotHttpClient(IOptions<EngageSpotOptions> options)
        {
            _options = options.Value;
        }

        public void Configure(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://api.engagespot.co/2/");
            httpClient.DefaultRequestHeaders.Add("Api-Key", _options.ApiKey);
        }
    }
}
