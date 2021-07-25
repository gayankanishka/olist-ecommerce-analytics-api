using System.Collections.Generic;
using System.Linq;
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
        private readonly IAnalyzerResultsBlobStorage _analyzerResultsBlobStorage;
        private readonly IConfiguration _configuration;

        public GetSalesPercentagesQueryHandler(IAnalyzerResultsBlobStorage analyzerResultsBlobStorage, IConfiguration configuration)
        {
            _analyzerResultsBlobStorage = analyzerResultsBlobStorage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<SalesPercentage>> Handle(GetSalesPercentagesQuery request,
            CancellationToken cancellationToken)
        {
            string filePath = _configuration.GetSection("HiveFiles")
                .GetSection("SalesPercentages")
                .Value;

            string result = await _analyzerResultsBlobStorage.DownloadAndReadBlobAsync(filePath);

            if (string.IsNullOrWhiteSpace(result))
            {
                return new List<SalesPercentage>();
            }

            IEnumerable<string> rows = result.Split('\n');

            if (rows.Any())
            {
                return rows
                    .Where(row => !string.IsNullOrWhiteSpace(row))
                    .Select(row => row.Split('\t'))
                    .Select(columns =>
                    {
                        return new SalesPercentage()
                        {
                            ProductId = columns[0],
                            CategoryName = columns[1],
                            Percentage = double.Parse(columns[2]),
                            SalesAmount = double.Parse(columns[3])
                        };
                    })
                    .ToList()
                    .OrderBy(_ => _.Percentage);
            }

            return new List<SalesPercentage>();
        }
    }
}
