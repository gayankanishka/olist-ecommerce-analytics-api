using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Domain.Mappings;

namespace Olist.Ecommerce.Analytics.Infrastructure.Files.Csv
{
    public class CsvMaterializer : ICsvMaterializer
    {
        public IEnumerable<T> MaterializeFile<T>(string localFilePath)
        {
            using StreamReader reader = new StreamReader(localFilePath);
            using CsvReader csv = new CsvReader(reader, CsvConfig);
            
            // HINT: Register all mappings here.
            csv.Context.RegisterClassMap<LocationMap>();

            return csv.GetRecords<T>().ToList();
        }

        private CsvConfiguration CsvConfig => new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            MissingFieldFound = null,
            TrimOptions = TrimOptions.Trim,
            BadDataFound = null
        };
    }
}
