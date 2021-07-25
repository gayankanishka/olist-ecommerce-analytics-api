using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.Infrastructure.Cloud.Blobs
{
    public class AnalyzerResultsBlobStorage : IAnalyzerResultsBlobStorage
    {
        private const string CONTAINER_NAME = "olist-analyzer-results";

        private readonly BlobContainerClient _blobContainerClient;

        public AnalyzerResultsBlobStorage(string connectionString)
        {
            _blobContainerClient = new BlobContainerClient(connectionString, CONTAINER_NAME);
            _blobContainerClient.CreateIfNotExists(PublicAccessType.BlobContainer);
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
