using System.Collections.Generic;
using MediatR;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages
{
    public class GetSalesPercentagesQuery : IRequest<IEnumerable<SalesPercentage>>
    {
    }
}
