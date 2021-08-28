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

namespace Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers
{
    /// <summary>
    /// Most popular sellers query handler.
    /// </summary>
    public class GetMostPopularSellersQueryHandler : 
        IRequestHandler<GetMostPopularSellersQuery, IEnumerable<MostPopularSellerDto>>
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
        public GetMostPopularSellersQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage, IConfiguration configuration,
            ICsvMaterializer csvMaterializer, ICacheStore cacheStore)
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
        public async Task<IEnumerable<MostPopularSellerDto>> Handle(GetMostPopularSellersQuery request,
            CancellationToken cancellationToken)
        {
            var sellers = _cacheStore.GetItem<IEnumerable<MostPopularSellerDto>>(CacheKeys.MostPopularSellers);

            if (sellers != null)
            {
                return sellers;
            }
            
            string blobFilePath = _configuration.GetSection("AnalyzerBlobStorage:MostPopularSellers").Value;
            string localFilePath = $"{Path.GetTempPath()}/{blobFilePath}";

            Response result = await _analyzerBlobStorage.DownloadBlobAsync(localFilePath, blobFilePath);

            sellers = result.Status != (int) HttpStatusCode.PartialContent 
                ? new List<MostPopularSellerDto>() 
                : _csvMaterializer.MaterializeFile<MostPopularSellerDto>(localFilePath).ToList();

            if (sellers.Any())
            {
                _cacheStore.AddItem(CacheKeys.MostPopularSellers, sellers);
            }

            return sellers;
        }
    }
}
