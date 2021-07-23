using System.Collections.Generic;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetMostSoldUsingCreditCardsProducts
{
    public class GetMostSoldUsingCreditCardsProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
