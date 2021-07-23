using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Constructor of LocationController.
        /// </summary>
        /// <param name="mediator"></param>
        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get most revenue generated locations.
        /// </summary>
        /// <returns>A list of most revenue generated locations.</returns>
        /// <response code="200">Most revenue generated locations list.</response>
        /// <response code="400">If invalid payload is passed</response>
        /// <response code="500">If something went wrong in the server-end</response>
        [Route("most-revenue")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Location>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostRevenueLocationsAsync()
        {
            IEnumerable<Location> locations = await _mediator.Send(
                new GetMostRevenueLocationsQuery());

            return Ok(locations);
        }
    }
}
