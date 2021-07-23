using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages
{
    public class GetSalesPercentagesQueryHandler :
        IRequestHandler<GetSalesPercentagesQuery, IEnumerable<SalesPercentage>>
    {
        private readonly IWebHdfsClient _webHdfsClient;
        private readonly IConfiguration _configuration;

        public GetSalesPercentagesQueryHandler(IWebHdfsClient webHdfsClient, IConfiguration configuration)
        {
            _webHdfsClient = webHdfsClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<SalesPercentage>> Handle(GetSalesPercentagesQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("HiveFiles")
                .GetSection("SalesPercentages")
                .Value;

            return await _webHdfsClient.OpenAndReadFileAsync<List<SalesPercentage>>(filePath);
        }
    }
}
