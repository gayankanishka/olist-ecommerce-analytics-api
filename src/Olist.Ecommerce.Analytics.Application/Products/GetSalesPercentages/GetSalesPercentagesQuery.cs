using System.Collections.Generic;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Enums;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages
{
    public class GetSalesPercentagesQuery : IRequest<IEnumerable<SalesPercentage>>
    {
        public DateFilters Filter { get; set; }

        public GetSalesPercentagesQuery(DateFilters filter)
        {
            Filter = filter;
        }
    }
}
