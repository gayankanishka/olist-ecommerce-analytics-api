using System.Threading.Tasks;
using Azure;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface IAnalyzerBlobStorage
    {
        Task<string> DownloadAndReadBlobAsync(string path);
        public Task<Response> DownloadBlobAsync(string localFilePath, string blobFilePath);
    }
}
