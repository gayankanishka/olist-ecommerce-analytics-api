using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Olist.Ecommerce.Analytics.Application.Locations.GetMostRevenueLocations;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    /// <summary>
    /// Handles Location related analytics operations.
    /// </summary>
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        
        /// <summary>
        /// Constructor of LocationController.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="memoryCache"></param>
        public LocationController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get most revenue generated locations.
        /// </summary>
        /// <returns>A list of most revenue generated locations.</returns>
        /// <response code="200">Most revenue generated locations list.</response>
        /// <response code="500">If something went wrong in the server-end</response>
        [Route("most-revenue")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Location>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostRevenueLocationsAsync()
        {
            var locations = await _mediator.Send(new GetMostRevenueLocationsQuery());
            return Ok(locations);
        }
    }
}
