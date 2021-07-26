using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.Infrastructure.Cloud.Blobs
{
    public class CloudBlobStorage : IAnalyzerBlobStorage
    {
        private readonly BlobContainerClient _blobContainerClient;

        public CloudBlobStorage(string connectionString, string containerName)
        {
            _blobContainerClient = new BlobContainerClient(connectionString, containerName);
        }

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
    }
}
