using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public GetMostPopularSellersQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage, IConfiguration configuration)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<MostPopularSellerDto>> Handle(GetMostPopularSellersQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("AnalyzerBlobStorage")
                .GetSection("MostPopularSellers")
                .Value;

            string result = await _analyzerBlobStorage.DownloadAndReadBlobAsync(filePath);

            if (string.IsNullOrWhiteSpace(result))
            {
                return new List<MostPopularSellerDto>();
            }

            IEnumerable<string> rows = result.Split('\n');

            if (rows.Any())
            {
                return rows
                    .Where(row => !string.IsNullOrWhiteSpace(row))
                    .Select(row => row.Split('\t'))
                    .Select(columns =>
                    {
                        return new MostPopularSellerDto()
                        {
                            ProductId = columns[0],
                            SellerId = columns[1]
                        };
                    })
                    .ToList();
            }

            return new List<MostPopularSellerDto>();
        }
    }
}
