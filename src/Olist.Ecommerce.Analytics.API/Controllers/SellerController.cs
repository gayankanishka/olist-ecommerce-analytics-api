using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers;
using Olist.Ecommerce.Analytics.Domain.Constants;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    /// <summary>
    /// Handles Seller related analytics operations.
    /// </summary>
    [Route("api/sellers")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Constructor of SellerController.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="memoryCache"></param>
        public SellerController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get most popular sellers along with the product details.
        /// </summary>
        /// <returns>A list of most popular sellers.</returns>
        /// <response code="200">Most popular sellers list.</response>
        /// <response code="400">If invalid payload is passed</response>
        /// <response code="500">If something went wrong in the server-end</response>
        [Route("most-popular")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MostPopularSellerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostPopularSellersAsync()
        {
            if (_memoryCache.TryGetValue(CacheKeys.MostPopularSellers, out IEnumerable<MostPopularSellerDto> sellers))
            {
                return Ok(sellers);
            }

            sellers = await _mediator.Send(new GetMostPopularSellersQuery());

            MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow.AddMinutes(60),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };

            _memoryCache.Set(CacheKeys.MostPopularSellers, sellers, cacheExpiryOptions);

            return Ok(sellers);
        }
    }
}
