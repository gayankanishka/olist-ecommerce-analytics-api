﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.Infrastructure.Hadoop
{
    /// <summary>
    /// Handles the interaction with the HDFS.
    /// </summary>
    public class WebHdfsClient : IWebHdfsClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="configuration"></param>
        public WebHdfsClient(HttpClient httpClient, IConfiguration configuration)
        {
            httpClient.BaseAddress = new Uri(configuration.GetSection("HdfsBaseUrl").Value);
            _httpClient = httpClient;

            SetDefaultHeaders();
        }

        /// <inheritdoc/>
        public async Task<T> OpenAndReadFileAsync<T>(string path)
        {
            string uri = $"/webhdfs/v1/{path}?op={HdfsOperations.OpenAndRead}";

            return await _httpClient.GetFromJsonAsync<T>(uri);
        }

        /// <inheritdoc/>
        public async Task<string> FileOrDirectoryStatusAsync(string path)
        {
            string uri = $"/webhdfs/v1/{path}?op={HdfsOperations.FileOrDirectoryStatus}";

            return await _httpClient.GetStringAsync(uri);
        }

        /// <inheritdoc/>
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
