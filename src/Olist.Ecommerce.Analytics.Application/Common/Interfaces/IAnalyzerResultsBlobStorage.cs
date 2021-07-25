using System.Threading.Tasks;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface IAnalyzerResultsBlobStorage
    {
        Task<string> DownloadAndReadBlobAsync(string path);
    }
}
