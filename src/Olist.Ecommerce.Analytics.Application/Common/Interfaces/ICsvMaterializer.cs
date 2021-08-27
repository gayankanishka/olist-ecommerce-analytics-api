using System.Collections.Generic;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    /// <summary>
    /// Handles the materialization of CSV files.
    /// </summary>
    public interface ICsvMaterializer
    {
        /// <summary>
        /// Reads the CSV file and returns a list of objects.
        /// </summary>
        /// <param name="localFilePath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of objects.</returns>
        IEnumerable<T> MaterializeFile<T>(string localFilePath);
    }
}