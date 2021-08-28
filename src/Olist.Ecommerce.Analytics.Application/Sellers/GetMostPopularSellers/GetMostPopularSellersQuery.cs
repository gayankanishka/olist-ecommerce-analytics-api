using System.Collections.Generic;
using MediatR;

namespace Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers
{
    /// <summary>
    /// Most popular sellers query.
    /// </summary>
    public class GetMostPopularSellersQuery : IRequest<IEnumerable<MostPopularSellerDto>>
    {
    }
}
