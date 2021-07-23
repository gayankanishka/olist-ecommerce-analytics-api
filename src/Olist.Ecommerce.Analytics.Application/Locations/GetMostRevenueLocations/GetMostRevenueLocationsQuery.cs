using System.Collections.Generic;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Locations.GetMostRevenueLocations
{
    public class GetMostRevenueLocationsQuery : IRequest<IEnumerable<Location>>
    {
    }
}
