using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Locations.GetMostRevenueLocations
{
    public class GetMostRevenueLocationsQueryHandler : 
        IRequestHandler<GetMostRevenueLocationsQuery, IEnumerable<Location>>
    {
        private readonly IWebHdfsClient _webHdfsClient;
        private readonly IConfiguration _configuration;

        public GetMostRevenueLocationsQueryHandler(IWebHdfsClient webHdfsClient, IConfiguration configuration)
        {
            _webHdfsClient = webHdfsClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Location>> Handle(GetMostRevenueLocationsQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("HiveFiles")
                .GetSection("MostRevenueLocations")
                .Value;

            return await _webHdfsClient.OpenAndReadFileAsync<List<Location>>(filePath);
        }
    }
}
