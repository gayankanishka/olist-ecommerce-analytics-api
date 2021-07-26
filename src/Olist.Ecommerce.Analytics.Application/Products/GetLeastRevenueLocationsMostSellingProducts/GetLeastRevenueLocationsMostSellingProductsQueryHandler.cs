using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public GetLeastRevenueLocationsMostSellingProductsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage,
            IConfiguration configuration)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<LeastRevenueLocationsMostSellingProductsDto>> Handle(GetLeastRevenueLocationsMostSellingProductsQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("AnalyzerBlobStorage")
                .GetSection("LeastRevenueLocationsMostSellingProducts")
                .Value;

            string result = await _analyzerBlobStorage.DownloadAndReadBlobAsync(filePath);

            if (string.IsNullOrWhiteSpace(result))
            {
                return new List<LeastRevenueLocationsMostSellingProductsDto>();
            }

            IEnumerable<string> rows = result.Split('\n');

            if (rows.Any())
            {
                return rows
                    .Where(row => !string.IsNullOrWhiteSpace(row))
                    .Select(row => row.Split('\t'))
                    .Select(columns =>
                    {
                        return new LeastRevenueLocationsMostSellingProductsDto()
                        {
                            ProductId = columns[0], 
                            State = columns[1], 
                            RankWithinState = int.Parse(columns[2]),
                            SalesPerProduct = float.Parse(columns[3])
                        };
                    })
                    .ToList()
                    .OrderBy(_ => _.RankWithinState);
            }

            return new List<LeastRevenueLocationsMostSellingProductsDto>();
        }
    }
}
