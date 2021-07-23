using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetMostSoldUsingCreditCardsProducts
{
    public class GetMostSoldUsingCreditCardsProductsQueryHandler :
        IRequestHandler<GetMostSoldUsingCreditCardsProductsQuery, IEnumerable<Product>>
    {
        private readonly IWebHdfsClient _webHdfsClient;
        private readonly IConfiguration _configuration;

        public GetMostSoldUsingCreditCardsProductsQueryHandler(IWebHdfsClient webHdfsClient, IConfiguration configuration)
        {
            _webHdfsClient = webHdfsClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> Handle(GetMostSoldUsingCreditCardsProductsQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("HiveFiles")
                .GetSection("MostSoldProductsUsingCreditCards")
                .Value;

            return await _webHdfsClient.OpenAndReadFileAsync<List<Product>>(filePath);
        }
    }
}
