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

namespace Olist.Ecommerce.Analytics.Application.Products.GetMostSoldProductsUsingCreditCards
{
    public class GetMostSoldProductsUsingCreditCardsQueryHandler :
        IRequestHandler<GetMostSoldProductsUsingCreditCardsQuery, IEnumerable<Product>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;
        private readonly ICsvMaterializer _csvMaterializer;

        public GetMostSoldProductsUsingCreditCardsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage,
            IConfiguration configuration, ICsvMaterializer csvMaterializer)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
            _csvMaterializer = csvMaterializer;
        }

        public async Task<IEnumerable<Product>> Handle(GetMostSoldProductsUsingCreditCardsQuery request,
            CancellationToken cancellationToken)
        {
            string blobFilePath = _configuration
                .GetSection("AnalyzerBlobStorage:MostSoldProductsUsingCreditCards").Value;
            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            return result.Status != (int)HttpStatusCode.PartialContent 
                ? new List<Product>() 
                : _csvMaterializer.MaterializeFile<Product>(localFilePath)
                    .OrderByDescending(_ => _.Count);
        }
    }
}
