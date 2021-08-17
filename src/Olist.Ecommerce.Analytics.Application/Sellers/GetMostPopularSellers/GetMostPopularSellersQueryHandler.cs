using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers
{
    public class GetMostPopularSellersQueryHandler : 
        IRequestHandler<GetMostPopularSellersQuery, IEnumerable<MostPopularSellerDto>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;
        private readonly ICsvMaterializer _csvMaterializer;

        public GetMostPopularSellersQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage, IConfiguration configuration,
            ICsvMaterializer csvMaterializer)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
            _csvMaterializer = csvMaterializer;
        }

        public async Task<IEnumerable<MostPopularSellerDto>> Handle(GetMostPopularSellersQuery request,
            CancellationToken cancellationToken)
        {
            string blobFilePath = _configuration.GetSection("AnalyzerBlobStorage:MostPopularSellers").Value;
            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            return result.Status != (int) HttpStatusCode.PartialContent 
                ? new List<MostPopularSellerDto>() 
                : _csvMaterializer.MaterializeFile<MostPopularSellerDto>(localFilePath);
        }
    }
}
