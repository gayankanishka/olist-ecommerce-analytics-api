using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olist.Ecommerce.Analytics.Application.Products.GetFastMovingProducts;
using Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages;
using Olist.Ecommerce.Analytics.Application.Products.GetSlowMovingProducts;
using Olist.Ecommerce.Analytics.Domain.Enums;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    /// <summary>
    /// Handles Products related analytics operations.
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor of ProductsController.
        /// </summary>
        /// <param name="mediator"></param>
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get fast selling products.
        /// </summary>
        /// <returns>A list of fast selling products.</returns>
        /// <response code="200">Fast selling products list.</response>
        /// <response code="400">If invalid payload is passed</response>
        /// <response code="500">If something went wrong in the server-end</response>
        [Route("fast-moving")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFastMovingProductsAsync()
        {
            IEnumerable<Product> products = await _mediator.Send(
                new GetFastMovingProductsQuery());

            return Ok(products);
        }

        /// <summary>
        /// Get slow selling products.
        /// </summary>
        /// <param name="locationId">The ID of the seller location.</param>
        /// <returns>A list of slow selling products along with users.</returns>
        /// <response code="200">Slow selling products list.</response>
        /// <response code="400">If invalid payload is passed.</response>
        /// <response code="500">If something went wrong in the server-end.</response>
        [Route("{locationId}/slow-moving")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSlowMovingProductsAsync([FromRoute] string locationId)
        {
            if (string.IsNullOrWhiteSpace(locationId))
            {
                return BadRequest("LocationId required!");
            }

            IEnumerable<Product> products = await _mediator.Send(
                new GetSlowMovingProductsQuery(locationId));

            return Ok(products);
        }
        
        /// <summary>
        /// Get sales percentages.
        /// </summary>
        /// <param name="filter">The <see cref="DateFilters"/> used to get relevant data.</param>
        /// <returns>A list of categories with sales percentages.</returns>
        /// <response code="200">Sales percentages list.</response>
        /// <response code="400">If invalid payload is passed.</response>
        /// <response code="500">If something went wrong in the server-end.</response>
        [Route("sales-percentages")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalesPercentage>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalesPercentagesAsync([FromQuery] DateFilters filter)
        {
            IEnumerable<SalesPercentage> salesPercentages = await _mediator.Send(
                new GetSalesPercentagesQuery(filter));

            return Ok(salesPercentages);
        }
    }
}
