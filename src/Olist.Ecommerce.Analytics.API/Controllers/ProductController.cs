using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts;
using Olist.Ecommerce.Analytics.Application.Products.GetMostSoldProductsUsingCreditCards;
using Olist.Ecommerce.Analytics.Application.Products.GetSalesPercentages;
using Olist.Ecommerce.Analytics.Domain.Constants;
using Olist.Ecommerce.Analytics.Domain.Enums;
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
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Constructor of ProductsController.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="memoryCache"></param>
        public ProductController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get least revenue locations most selling products.
        /// </summary>
        /// <returns>A list of most selling products in least revenue locations.</returns>
        /// <response code="200">Most selling products.</response>
        /// <response code="400">If invalid payload is passed.</response>
        /// <response code="500">If something went wrong in the server-end.</response>
        [Route("least-revenue-locations-most-selling")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LeastRevenueLocationsMostSellingProductsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLeastRevenueLocationsMostSellingProductsAsync()
        {
            if (_memoryCache.TryGetValue(CacheKeys.LeastRevenueLocationsMostSellingProducts,
                out IEnumerable<LeastRevenueLocationsMostSellingProductsDto> products))
            {
                return Ok(products);
            }

            products = await _mediator.Send(new GetLeastRevenueLocationsMostSellingProductsQuery());

            _memoryCache.Set(CacheKeys.LeastRevenueLocationsMostSellingProducts, products, MemoryCacheOptions);

            return Ok(products);
        }

        /// <summary>
        /// Get most sold products using credit cards.
        /// </summary>
        /// <returns>A list of products.</returns>
        /// <response code="200">Products list.</response>
        /// <response code="400">If invalid payload is passed.</response>
        /// <response code="500">If something went wrong in the server-end.</response>
        [Route("most-sold-using-credit-cards")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostSoldProductsUsingCreditCardsAsync()
        {
            if (_memoryCache.TryGetValue(CacheKeys.MostSoldProductsUsingCreditCards, out IEnumerable<Product> products))
            {
                return Ok(products);
            }

            products = await _mediator.Send(new GetMostSoldProductsUsingCreditCardsQuery());

            _memoryCache.Set(CacheKeys.MostSoldProductsUsingCreditCards, products, MemoryCacheOptions);

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
            if (_memoryCache.TryGetValue(CacheKeys.SalesPercentages, out IEnumerable<SalesPercentage> salesPercentages))
            {
                return Ok(salesPercentages);
            }

            salesPercentages = await _mediator.Send(new GetSalesPercentagesQuery(filter));

            _memoryCache.Set(CacheKeys.SalesPercentages, salesPercentages, MemoryCacheOptions);

            return Ok(salesPercentages);
        }

        private MemoryCacheEntryOptions MemoryCacheOptions => new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.UtcNow.AddMinutes(60),
            Priority = CacheItemPriority.High,
            SlidingExpiration = TimeSpan.FromMinutes(30)
        };
    }
}
