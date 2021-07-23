using System.Collections.Generic;
using MediatR;

namespace Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers
{
    public class GetMostPopularSellersQuery : IRequest<IEnumerable<MostPopularSellerDto>>
    {
    }
}
