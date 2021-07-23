using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetMostSoldUsingCreditCardsProducts
{
    public class GetMostSoldUsingCreditCardsProductsQueryHandler :
        IRequestHandler<GetMostSoldUsingCreditCardsProductsQuery, IEnumerable<Product>>
    {
        public Task<IEnumerable<Product>> Handle(GetMostSoldUsingCreditCardsProductsQuery request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
