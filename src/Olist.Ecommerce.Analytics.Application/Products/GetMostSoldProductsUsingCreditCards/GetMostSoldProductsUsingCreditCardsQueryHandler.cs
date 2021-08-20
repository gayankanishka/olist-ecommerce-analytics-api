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
using Olist.Ecommerce.Analytics.Domain.Constants;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetMostSoldProductsUsingCreditCards
{
    public class GetMostSoldProductsUsingCreditCardsQueryHandler :
        IRequestHandler<GetMostSoldProductsUsingCreditCardsQuery, IEnumerable<Product>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;
        private readonly ICsvMaterializer _csvMaterializer;
        private readonly ICacheStore _cacheStore;

        public GetMostSoldProductsUsingCreditCardsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage,
            IConfiguration configuration, ICsvMaterializer csvMaterializer, ICacheStore cacheStore)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
            _csvMaterializer = csvMaterializer;
            _cacheStore = cacheStore;
        }

        public async Task<IEnumerable<Product>> Handle(GetMostSoldProductsUsingCreditCardsQuery request,
            CancellationToken cancellationToken)
        {
            var products = _cacheStore.GetItem<IEnumerable<Product>>(CacheKeys.MostSoldProductsUsingCreditCards);

            if (products != null)
            {
                return products;
            }
            
            string blobFilePath = _configuration
                .GetSection("AnalyzerBlobStorage:MostSoldProductsUsingCreditCards").Value;
            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            products = result.Status != (int)HttpStatusCode.PartialContent 
                ? new List<Product>() 
                : _csvMaterializer.MaterializeFile<Product>(localFilePath)
                    .OrderByDescending(_ => _.Count)
                    .ToList();

            if (products.Any())
            {
                _cacheStore.AddItem(CacheKeys.MostSoldProductsUsingCreditCards, products);
            }
            
            return products;
        }
    }
}
