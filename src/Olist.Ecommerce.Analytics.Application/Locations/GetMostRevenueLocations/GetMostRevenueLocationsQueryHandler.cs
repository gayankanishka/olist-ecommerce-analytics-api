using System.Collections.Generic;
using System.Linq;
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

            string result =
                "9ef432eb6251297304e76186b10a928d\t3\t1252\nb0830fb4747a6c6d20dea0b8c802d7ef\t2\t1254\n41ce2a54c0b03bf3443c3d931a367089\t1\t1545";

            //string result = await _webHdfsClient.OpenAndReadFileAsync<string>(filePath);

            if (string.IsNullOrWhiteSpace(result))
            {
                return new List<Location>();
            }

            IEnumerable<string> rows = result.Split('\n');

            if (rows.Any())
            {
                return rows
                    .Where(row => !string.IsNullOrWhiteSpace(row))
                    .Select(row => row.Split('\t'))
                    .Select(columns =>
                    {
                        return new Location
                        {
                            City = columns[0],
                            Rank = int.Parse(columns[1]),
                            Revenue = double.Parse(columns[2])
                        };
                    })
                    .ToList()
                    .OrderBy(_ => _.Rank);
            }

            return new List<Location>();
        }
    }
}
