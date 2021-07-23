using System.Threading.Tasks;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface IWebHdfsClient
    {
        Task<T> OpenAndReadFileAsync<T>(string path) where T : class, new();
        Task<string> FileOrDirectoryStatusAsync(string path);
        Task<string> ListDirectoryAsync(string path);
    }
}
