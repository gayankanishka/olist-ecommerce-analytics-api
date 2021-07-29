using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    public class GetLeastRevenueLocationsMostSellingProductsQueryHandler :
        IRequestHandler<GetLeastRevenueLocationsMostSellingProductsQuery, IEnumerable<LeastRevenueLocationsMostSellingProductsDto>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;

        public GetLeastRevenueLocationsMostSellingProductsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage,
            IConfiguration configuration)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<LeastRevenueLocationsMostSellingProductsDto>> Handle(GetLeastRevenueLocationsMostSellingProductsQuery request,
            CancellationToken cancellationToken)
        {
            string blobFilePath = _configuration.GetSection("AnalyzerBlobStorage")
                .GetSection("LeastRevenueLocationsMostSellingProducts")
                .Value;

            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = 
                await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            if (result.Status != (int) HttpStatusCode.PartialContent)
            {
                return new List<LeastRevenueLocationsMostSellingProductsDto>();
            }
            
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = "\t",
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null
            };

            using StreamReader reader = new StreamReader(localFilePath);
            using CsvReader csv = new CsvReader(reader, config);
            
            return csv.GetRecords<LeastRevenueLocationsMostSellingProductsDto>()
                .OrderBy(_ => _.RankWithinState)
                .ToList();
        }
    }
}
