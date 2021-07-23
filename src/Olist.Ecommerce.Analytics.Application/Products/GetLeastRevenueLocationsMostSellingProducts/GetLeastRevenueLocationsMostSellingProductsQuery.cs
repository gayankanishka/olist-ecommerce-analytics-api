using System.Collections.Generic;
using MediatR;

namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    public class GetLeastRevenueLocationsMostSellingProductsQuery :
        IRequest<IEnumerable<LeastRevenueLocationsMostSellingProductsDto>>
    {
    }
}
