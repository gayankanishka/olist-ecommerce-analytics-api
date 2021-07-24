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
        private readonly IWebHdfsClient _webHdfsClient;
        private readonly IConfiguration _configuration;

        public GetMostSoldProductsUsingCreditCardsQueryHandler(IWebHdfsClient webHdfsClient, IConfiguration configuration)
        {
            _webHdfsClient = webHdfsClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> Handle(GetMostSoldProductsUsingCreditCardsQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("HiveFiles")
                .GetSection("MostSoldProductsUsingCreditCards")
                .Value;

            string result =
                "9ef432eb6251297304e76186b10a928d\tsp\t20\nb0830fb4747a6c6d20dea0b8c802d7ef\tca\t200\n41ce2a54c0b03bf3443c3d931a367089\tla\t3000\n";

            // string result = await _webHdfsClient.OpenAndReadFileAsync<string>(filePath);

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
