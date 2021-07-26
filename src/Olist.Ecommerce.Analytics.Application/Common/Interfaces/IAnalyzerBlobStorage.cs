using System.Threading.Tasks;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface IAnalyzerBlobStorage
    {
        Task<string> DownloadAndReadBlobAsync(string path);
    }
}
