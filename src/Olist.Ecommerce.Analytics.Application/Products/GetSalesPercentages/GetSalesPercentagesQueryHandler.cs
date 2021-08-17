using System;
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

namespace Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages
{
    public class GetSalesPercentagesQueryHandler :
        IRequestHandler<GetSalesPercentagesQuery, IEnumerable<SalesPercentage>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;
        private readonly ICsvMaterializer _csvMaterializer;

        public GetSalesPercentagesQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage, IConfiguration configuration,
            ICsvMaterializer csvMaterializer)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
            _csvMaterializer = csvMaterializer;
        }

        public async Task<IEnumerable<SalesPercentage>> Handle(GetSalesPercentagesQuery request,
            CancellationToken cancellationToken)
        {
            string blobFilePath = _configuration.GetSection("AnalyzerBlobStorage:SalesPercentages").Value;
            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            return result.Status != (int)HttpStatusCode.PartialContent
                ? new List<SalesPercentage>()
                : _csvMaterializer.MaterializeFile<SalesPercentage>(localFilePath)
                    .Select(_ =>
                    {
                        _.Percentage = Math.Round(_.Percentage, 2);
                        _.SalesAmount = Math.Round(_.SalesAmount, 2);
                        return _;
                    })
                    .OrderByDescending(_ => _.Percentage);
        }
    }
}
