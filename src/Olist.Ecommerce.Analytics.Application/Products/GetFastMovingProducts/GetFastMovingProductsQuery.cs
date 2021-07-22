using System.Collections.Generic;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetFastMovingProducts
{
    public class GetFastMovingProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
