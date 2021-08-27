using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.Infrastructure.Cloud.Blobs
{
    /// <summary>
    /// Handles the blob storage operations.
    /// </summary>
    public class CloudBlobStorage : IAnalyzerBlobStorage
    {
        private readonly BlobContainerClient _blobContainerClient;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="containerName"></param>
        public CloudBlobStorage(string connectionString, string containerName)
        {
            _blobContainerClient = new BlobContainerClient(connectionString, containerName);
        }

        /// <inheritdoc/>
        public async Task<string> DownloadAndReadBlobAsync(string pathAndFileName)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(pathAndFileName);

            if (!await blobClient.ExistsAsync())
            {
                return string.Empty;
            }

            BlobDownloadInfo download = await blobClient.DownloadAsync();
            byte[] result = new byte[download.ContentLength];
            await download.Content.ReadAsync(result, 0, (int)download.ContentLength);
 
            return Encoding.UTF8.GetString(result);
        }

        /// <inheritdoc/>
        public async Task<Response> DownloadBlobAsync(string localFilePath, string blobFilePath)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(blobFilePath);
            
            return await blobClient.DownloadToAsync(localFilePath);
        }
    }
}
