using System.Collections.Generic;
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

            return await _webHdfsClient.OpenAndReadFileAsync<List<MostPopularSellerDto>>(filePath);
        }
    }
}
