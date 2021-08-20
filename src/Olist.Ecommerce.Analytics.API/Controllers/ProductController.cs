using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts;
using Olist.Ecommerce.Analytics.Application.Products.GetMostSoldProductsUsingCreditCards;
using Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    /// <summary>
    /// Handles Product related analytics operations.
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
        /// Get least revenue locations most selling products.
        /// </summary>
        /// <returns>A list of most selling products in least revenue locations.</returns>
        /// <response code="200">Most selling products.</response>
        /// <response code="500">If something went wrong in the server-end.</response>
        [Route("least-revenue-locations-most-selling")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LeastRevenueLocationsMostSellingProductsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLeastRevenueLocationsMostSellingProductsAsync()
        {
            var products = 
                await _mediator.Send(new GetLeastRevenueLocationsMostSellingProductsQuery());
            
            return Ok(products);
        }

        /// <summary>
        /// Get most sold products using credit cards.
        /// </summary>
        /// <returns>A list of products.</returns>
        /// <response code="200">Products list.</response>
        /// <response code="500">If something went wrong in the server-end.</response>
        [Route("most-sold-using-credit-cards")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostSoldProductsUsingCreditCardsAsync()
        {
            var products = await _mediator.Send(new GetMostSoldProductsUsingCreditCardsQuery());
            
            return Ok(products);
        }
        
        /// <summary>
        /// Get sales percentages.
        /// </summary>
        /// <returns>A list of categories with sales percentages.</returns>
        /// <response code="200">Sales percentages list.</response>
        /// <response code="500">If something went wrong in the server-end.</response>
        [Route("sales-percentages")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalesPercentage>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalesPercentagesAsync()
        {
            var salesPercentages = await _mediator.Send(new GetSalesPercentagesQuery());

            return Ok(salesPercentages);
        }
    }
}
