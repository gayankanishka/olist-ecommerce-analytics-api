using System.Threading.Tasks;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    /// <summary>
    /// Handles the interaction with the HDFS.
    /// </summary>
    public interface IWebHdfsClient
    {
        /// <summary>
        /// Opens and reads the content of a file.
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> OpenAndReadFileAsync<T>(string path);
        
        /// <summary>
        /// View the status of a file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> FileOrDirectoryStatusAsync(string path);
        
        /// <summary>
        /// Lists the content of a directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> ListDirectoryAsync(string path);
    }
}
