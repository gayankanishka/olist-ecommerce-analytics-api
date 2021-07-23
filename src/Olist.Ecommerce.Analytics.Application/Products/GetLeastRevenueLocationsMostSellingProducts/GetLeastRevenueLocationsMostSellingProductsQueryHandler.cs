using System.Collections.Generic;
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

            return await _webHdfsClient.OpenAndReadFileAsync<List<LeastRevenueLocationsMostSellingProductsDto>>(filePath);
        }
    }
}
