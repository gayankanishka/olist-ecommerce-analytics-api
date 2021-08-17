﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Locations.GetMostRevenueLocations
{
    public class GetMostRevenueLocationsQueryHandler :
        IRequestHandler<GetMostRevenueLocationsQuery, IEnumerable<Location>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;
        private readonly ICsvMaterializer _csvMaterializer;

        public GetMostRevenueLocationsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage,
            IConfiguration configuration, ICsvMaterializer csvMaterializer)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
            _csvMaterializer = csvMaterializer;
        }

        public async Task<IEnumerable<Location>> Handle(GetMostRevenueLocationsQuery request,
            CancellationToken cancellationToken)
        {
            string blobFilePath = _configuration.GetSection("AnalyzerBlobStorage:MostRevenueLocations").Value;
            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            int rank = 1;

            return result.Status != (int)HttpStatusCode.PartialContent
                ? new List<Location>()
                : _csvMaterializer.MaterializeFile<Location>(localFilePath)
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
