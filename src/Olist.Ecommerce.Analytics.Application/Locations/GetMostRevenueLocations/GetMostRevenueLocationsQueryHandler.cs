using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Locations.GetMostRevenueLocations
{
    public class GetMostRevenueLocationsQueryHandler : 
        IRequestHandler<GetMostRevenueLocationsQuery, IEnumerable<Location>>
    {
        public Task<IEnumerable<Location>> Handle(GetMostRevenueLocationsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
