using System;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<string> OpenAndReadFileAsync(string path)
        {
            string uri = $"/webhdfs/v1/{path}?op={HdfsOperations.OPEN_AND_READ}";

            return await _httpClient.GetStringAsync(uri);
        }

        public async Task<string> FileOrDirectoryStatusAsync(string path)
        {
            string uri = $"/webhdfs/v1/{path}?op={HdfsOperations.FILE_OR_DIRECTORY_STATUS}";

            return await _httpClient.GetStringAsync(uri);
        }

        public async Task<string> ListDirectoryAsync(string path)
        {
            string uri = $"/webhdfs/v1/{path}?op={HdfsOperations.LIST_DIRECTORY}";

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
