using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetSlowMovingProducts
{
    public class GetSlowMovingProductsQueryHandler :
        IRequestHandler<GetSlowMovingProductsQuery, IEnumerable<Product>>
    {
        public Task<IEnumerable<Product>> Handle(GetSlowMovingProductsQuery request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
