using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olist.Ecommerce.Analytics.Application.Products.GetFastMovingProducts;
using Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages;
using Olist.Ecommerce.Analytics.Application.Products.GetSlowMovingProducts;
using Olist.Ecommerce.Analytics.Domain.Enums;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("fast-moving")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetFastMovingProductsAsync()
        {
            // TODO: This should return top selling products
            IEnumerable<Product> products = await _mediator.Send(new GetFastMovingProductsQuery());

            return Ok(products);
        }

        [Route("{locationId}/slow-moving")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSlowMovingProductsAsync([FromRoute] string locationId)
        {
            // TODO: This should return the slow moving items as objects and users as objects alongside

            if (string.IsNullOrWhiteSpace(locationId))
            {
                return BadRequest("LocationId required!");
            }

            IEnumerable<Product> products = await _mediator.Send(
                new GetSlowMovingProductsQuery(locationId));

            return Ok(products);
        }

        [Route("sales-percentages")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalesPercentage>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSalesPercentagesAsync([FromQuery] DateFilters filter)
        {
            // TODO: This should return a list of categories with percentages

            IEnumerable<SalesPercentage> salesPercentages = await _mediator.Send(
                new GetSalesPercentagesQuery(filter));

            return Ok(salesPercentages);
        }
    }
}
