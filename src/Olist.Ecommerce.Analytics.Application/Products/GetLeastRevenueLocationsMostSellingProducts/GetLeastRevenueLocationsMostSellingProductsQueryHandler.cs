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

namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    public class GetLeastRevenueLocationsMostSellingProductsQueryHandler :
        IRequestHandler<GetLeastRevenueLocationsMostSellingProductsQuery, IEnumerable<LeastRevenueLocationsMostSellingProductsDto>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;
        private readonly ICsvMaterializer _csvMaterializer;

        public GetLeastRevenueLocationsMostSellingProductsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage,
            IConfiguration configuration, ICsvMaterializer csvMaterializer)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
            _csvMaterializer = csvMaterializer;
        }

        public async Task<IEnumerable<LeastRevenueLocationsMostSellingProductsDto>> Handle(GetLeastRevenueLocationsMostSellingProductsQuery request,
            CancellationToken cancellationToken)
        {
            string blobFilePath = _configuration
                .GetSection("AnalyzerBlobStorage:LeastRevenueLocationsMostSellingProducts").Value;
            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            return result.Status != (int)HttpStatusCode.PartialContent
                ? new List<LeastRevenueLocationsMostSellingProductsDto>()
                : _csvMaterializer.MaterializeFile<LeastRevenueLocationsMostSellingProductsDto>(localFilePath)
                    .OrderBy(_ => _.RankWithinState);
        }
    }
}
