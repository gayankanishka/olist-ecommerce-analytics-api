using System.Collections.Generic;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetSlowMovingProducts
{
    public class GetSlowMovingProductsQuery : IRequest<IEnumerable<Product>>
    {
        public string LocationId { get; }

        public GetSlowMovingProductsQuery(string locationId)
        {
            LocationId = locationId;
        }
    }
}
