using System;
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
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages
{
    public class GetSalesPercentagesQueryHandler :
        IRequestHandler<GetSalesPercentagesQuery, IEnumerable<SalesPercentage>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;

        public GetSalesPercentagesQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage, IConfiguration configuration)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<SalesPercentage>> Handle(GetSalesPercentagesQuery request,
            CancellationToken cancellationToken)
        {
            string blobFilePath = _configuration.GetSection("AnalyzerBlobStorage")
                .GetSection("SalesPercentages")
                .Value;

            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = 
                await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            if (result.Status != (int) HttpStatusCode.PartialContent)
            {
                return new List<SalesPercentage>();
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
            
            return csv.GetRecords<SalesPercentage>()
                .Select(_ =>
                {
                    _.Percentage = Math.Round(_.Percentage, 2);
                    _.SalesAmount = Math.Round(_.SalesAmount, 2);
                    return _;
                })
                .OrderByDescending(_ => _.Percentage)
                .ToList();
        }
    }
}
