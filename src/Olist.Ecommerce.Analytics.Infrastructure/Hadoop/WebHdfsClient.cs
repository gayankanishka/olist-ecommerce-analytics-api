using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.Infrastructure.Hadoop
{
    public class WebHdfsClient : IWebHdfsClient
    {
        private readonly HttpClient _httpClient;

        public WebHdfsClient(HttpClient httpClient, IConfiguration configuration)
        {
            httpClient.BaseAddress = new Uri(configuration.GetSection("HdfsBaseUrl").Value);
            _httpClient = httpClient;

            SetDefaultHeaders();
        }

        public async Task<T> OpenAndReadFileAsync<T>(string path) where T : class, new()
        {
            string uri = $"/webhdfs/v1/{path}?op={HdfsOperations.OpenAndRead}";

            return await _httpClient.GetFromJsonAsync<T>(uri);
        }

        public async Task<string> FileOrDirectoryStatusAsync(string path)
        {
            string uri = $"/webhdfs/v1/{path}?op={HdfsOperations.FileOrDirectoryStatus}";

            return await _httpClient.GetStringAsync(uri);
        }

        public async Task<string> ListDirectoryAsync(string path)
        {
            string uri = $"/webhdfs/v1/{path}?op={HdfsOperations.ListDirectory}";

            return await _httpClient.GetStringAsync(uri);
        }

        private void SetDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
