using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers
{
    public class GetMostPopularSellersQueryHandler : 
        IRequestHandler<GetMostPopularSellersQuery, IEnumerable<MostPopularSellerDto>>
    {
        public Task<IEnumerable<MostPopularSellerDto>> Handle(GetMostPopularSellersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
