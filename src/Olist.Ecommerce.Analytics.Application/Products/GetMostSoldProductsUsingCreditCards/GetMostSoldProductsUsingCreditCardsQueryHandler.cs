using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public GetMostSoldProductsUsingCreditCardsQueryHandler(IAnalyzerBlobStorage analyzerBlobStorage, IConfiguration configuration)
        {
            _analyzerBlobStorage = analyzerBlobStorage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> Handle(GetMostSoldProductsUsingCreditCardsQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("AnalyzerBlobStorage")
                .GetSection("MostSoldProductsUsingCreditCards")
                .Value;

            string result = await _analyzerBlobStorage.DownloadAndReadBlobAsync(filePath);

            if (string.IsNullOrWhiteSpace(result))
            {
                return new List<Product>();
            }

            IEnumerable<string> rows = result.Split('\n');

            if (rows.Any())
            {
                return rows
                    .Where(row => !string.IsNullOrWhiteSpace(row))
                    .Select(row => row.Split('\t'))
                    .Select(columns =>
                    {
                        return new Product()
                        {
                            ProductId = columns[0],
                            CategoryName = columns[1],
                            Count = int.Parse(columns[2])
                        };
                    })
                    .ToList()
                    .OrderBy(_ => _.Count);
            }

            return new List<Product>();
        }
    }
}
