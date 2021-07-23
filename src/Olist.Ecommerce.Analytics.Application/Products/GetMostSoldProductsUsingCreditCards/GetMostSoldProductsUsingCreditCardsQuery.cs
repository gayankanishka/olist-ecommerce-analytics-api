using System.Collections.Generic;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetMostSoldProductsUsingCreditCards
{
    public class GetMostSoldProductsUsingCreditCardsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
