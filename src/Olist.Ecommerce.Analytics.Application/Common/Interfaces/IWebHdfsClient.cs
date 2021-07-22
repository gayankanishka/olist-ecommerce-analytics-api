using System.Threading.Tasks;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface IWebHdfsClient
    {
        Task<string> OpenAndReadFileAsync(string path);
        Task<string> FileOrDirectoryStatusAsync(string path);
        Task<string> ListDirectoryAsync(string path);
    }
}
