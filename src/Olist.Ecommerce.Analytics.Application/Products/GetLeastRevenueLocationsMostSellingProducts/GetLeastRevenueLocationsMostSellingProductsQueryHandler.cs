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
        private readonly IWebHdfsClient _webHdfsClient;
        private readonly IConfiguration _configuration;

        public GetLeastRevenueLocationsMostSellingProductsQueryHandler(IWebHdfsClient webHdfsClient, IConfiguration configuration)
        {
            _webHdfsClient = webHdfsClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<LeastRevenueLocationsMostSellingProductsDto>> Handle(GetLeastRevenueLocationsMostSellingProductsQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("HiveFiles")
                .GetSection("LeastRevenueLocationsMostSellingProducts")
                .Value;

            string result =
                "9ef432eb6251297304e76186b10a928d\tsp\t2\t1254\nb0830fb4747a6c6d20dea0b8c802d7ef\tca\t1\t665.23\n41ce2a54c0b03bf3443c3d931a367089\tla\t3\t5556.32";

            //string result = await _webHdfsClient.OpenAndReadFileAsync<List<string>>(filePath);

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
