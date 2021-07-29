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
using Olist.Ecommerce.Analytics.Domain.Mappings;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Locations.GetMostRevenueLocations
{
    public class GetMostRevenueLocationsQueryHandler :
        IRequestHandler<GetMostRevenueLocationsQuery, IEnumerable<Location>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;

        public GetMostRevenueLocationsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage, IConfiguration configuration)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Location>> Handle(GetMostRevenueLocationsQuery request,
            CancellationToken cancellationToken)
        {
            // TODO: Refactor all handlers
            
            string blobFilePath = _configuration.GetSection("AnalyzerBlobStorage")
                .GetSection("MostRevenueLocations")
                .Value;

            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = 
                await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            if (result.Status != (int) HttpStatusCode.PartialContent)
            {
                return new List<Location>();
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
            
            csv.Context.RegisterClassMap<LocationMap>();

            int rank = 1;
            
            return csv.GetRecords<Location>()
                .Select(_ =>
                {
                    _.Revenue = Math.Round(_.Revenue, 2);
                    return _;
                })
                .OrderByDescending(_ => _.Revenue)
                .Select(_ =>
                {
                    _.Rank += rank++;
                    return _;
                })
                .ToList();
        }
    }
}
