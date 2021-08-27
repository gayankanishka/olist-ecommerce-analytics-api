using System.Threading.Tasks;
using Azure;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    /// <summary>
    /// Handles the blob storage operations.
    /// </summary>
    public interface IAnalyzerBlobStorage
    {
        /// <summary>
        /// Downloads the blob and returns the content as string.
        /// </summary>
        /// <param name="path">File path to the blob.</param>
        /// <returns>String Content.</returns>
        Task<string> DownloadAndReadBlobAsync(string path);
        
        /// <summary>
        /// Downloads the blob and stores locally.
        /// </summary>
        /// <param name="localFilePath">Destination file path.</param>
        /// <param name="blobFilePath">File path to the blob.</param>
        /// <returns>Azure storage response.</returns>
        public Task<Response> DownloadBlobAsync(string localFilePath, string blobFilePath);
    }
}
