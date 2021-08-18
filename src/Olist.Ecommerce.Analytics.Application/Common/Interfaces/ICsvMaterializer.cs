using System.Collections.Generic;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface ICsvMaterializer
    {
        IEnumerable<T> MaterializeFile<T>(string localFilePath);
    }
}