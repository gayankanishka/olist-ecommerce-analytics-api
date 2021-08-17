using System.Collections.Generic;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface ICsvMaterializer
    {
        IList<T> MaterializeFile<T>(string localFilePath);
    }
}