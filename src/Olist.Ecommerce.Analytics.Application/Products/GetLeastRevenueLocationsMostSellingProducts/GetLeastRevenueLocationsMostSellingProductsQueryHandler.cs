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

namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    /// <summary>
    /// Least revenue location query handler.
    /// </summary>
    public class GetLeastRevenueLocationsMostSellingProductsQueryHandler :
        IRequestHandler<GetLeastRevenueLocationsMostSellingProductsQuery, IEnumerable<LeastRevenueLocationsMostSellingProductsDto>>
    {
        private readonly IAnalyzerBlobStorage _analyzerBlobStorage;
        private readonly IConfiguration _configuration;
        private readonly ICsvMaterializer _csvMaterializer;
        private readonly ICacheStore _cacheStore;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="analyzerBlobStorage"></param>
        /// <param name="configuration"></param>
        /// <param name="csvMaterializer"></param>
        /// <param name="cacheStore"></param>
        public GetLeastRevenueLocationsMostSellingProductsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage,
            IConfiguration configuration, ICsvMaterializer csvMaterializer, ICacheStore cacheStore)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
            _csvMaterializer = csvMaterializer;
            _cacheStore = cacheStore;
        }

        /// <summary>
        /// Query handler.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LeastRevenueLocationsMostSellingProductsDto>> Handle(GetLeastRevenueLocationsMostSellingProductsQuery request,
            CancellationToken cancellationToken)
        {
            var products = _cacheStore.GetItem<IEnumerable<LeastRevenueLocationsMostSellingProductsDto>>
                (CacheKeys.LeastRevenueLocationsMostSellingProducts);

            if (products != null)
            {
                return products;
            }
            
            string blobFilePath = _configuration
                .GetSection("AnalyzerBlobStorage:LeastRevenueLocationsMostSellingProducts").Value;
            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            products = result.Status != (int)HttpStatusCode.PartialContent
                ? new List<LeastRevenueLocationsMostSellingProductsDto>()
                : _csvMaterializer.MaterializeFile<LeastRevenueLocationsMostSellingProductsDto>(localFilePath)
                    .OrderBy(_ => _.RankWithinState)
                    .ToList();

            if (products.Any())
            {
                _cacheStore.AddItem(CacheKeys.LeastRevenueLocationsMostSellingProducts, products);
            }

            return products;
        }
    }
}
