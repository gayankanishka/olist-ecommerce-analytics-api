using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetFastMovingProducts
{
    public class GetFastMovingProductsQueryHandler :
        IRequestHandler<GetFastMovingProductsQuery, IEnumerable<Product>>
    {
        public async Task<IEnumerable<Product>> Handle(GetFastMovingProductsQuery request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
