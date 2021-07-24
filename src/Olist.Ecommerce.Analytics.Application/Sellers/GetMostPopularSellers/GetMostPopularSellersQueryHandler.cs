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
        private readonly IWebHdfsClient _webHdfsClient;
        private readonly IConfiguration _configuration;

        public GetMostPopularSellersQueryHandler(IWebHdfsClient webHdfsClient, IConfiguration configuration)
        {
            _webHdfsClient = webHdfsClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<MostPopularSellerDto>> Handle(GetMostPopularSellersQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("HiveFiles")
                .GetSection("MostPopularSellers")
                .Value;

            string result =
                "9ef432eb6251297304e76186b10a928d\t31ad1d1b63eb9962463f764d4e6e0c9d\nb0830fb4747a6c6d20dea0b8c802d7ef\tf54a9f0e6b351c431402b8461ea51999\n41ce2a54c0b03bf3443c3d931a367089\t9bdf08b4b3b52b5526ff42d37d47f222";

            // string result = await _webHdfsClient.OpenAndReadFileAsync<string>(filePath);

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
