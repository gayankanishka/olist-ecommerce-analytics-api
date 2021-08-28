using System.Collections.Generic;
using MediatR;

namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    /// <summary>
    /// Least revenue location query.
    /// </summary>
    public class GetLeastRevenueLocationsMostSellingProductsQuery :
        IRequest<IEnumerable<LeastRevenueLocationsMostSellingProductsDto>>
    {
    }
}
