﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Locations.GetMostRevenueLocations
{
    public class GetMostRevenueLocationsQueryHandler :
        IRequestHandler<GetMostRevenueLocationsQuery, IEnumerable<Location>>
    {
        private readonly IAnalyzerResultsBlobStorage _analyzerResultsBlobStorage;
        private readonly IConfiguration _configuration;

        public GetMostRevenueLocationsQueryHandler(IAnalyzerResultsBlobStorage analyzerResultsBlobStorage, IConfiguration configuration)
        {
            _analyzerResultsBlobStorage = analyzerResultsBlobStorage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Location>> Handle(GetMostRevenueLocationsQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("HiveFiles")
                .GetSection("MostRevenueLocations")
                .Value;

            string result = await _analyzerResultsBlobStorage.DownloadAndReadBlobAsync(filePath);

            if (string.IsNullOrWhiteSpace(result))
            {
                return new List<Location>();
            }

            IEnumerable<string> rows = result.Split('\n');

            if (rows.Any())
            {
                return rows
                    .Where(row => !string.IsNullOrWhiteSpace(row))
                    .Select(row => row.Split('\t'))
                    .Select(columns =>
                    {
                        return new Location
                        {
                            City = columns[0],
                            Rank = int.Parse(columns[1]),
                            Revenue = double.Parse(columns[2])
                        };
                    })
                    .ToList()
                    .OrderBy(_ => _.Rank);
            }

            return new List<Location>();
        }
    }
}
