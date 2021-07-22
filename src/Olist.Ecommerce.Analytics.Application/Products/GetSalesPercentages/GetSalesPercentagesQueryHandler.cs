using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Enums;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages
{
    public class GetSalesPercentagesQueryHandler :
        IRequestHandler<GetSalesPercentagesQuery, IEnumerable<SalesPercentage>>
    {
        public Task<IEnumerable<SalesPercentage>> Handle(GetSalesPercentagesQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Filter == DateFilters.None)
            {
            }

            throw new NotImplementedException();
        }
    }
}
