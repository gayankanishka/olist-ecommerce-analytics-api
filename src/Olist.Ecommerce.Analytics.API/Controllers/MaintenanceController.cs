using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    /// <summary>
    /// Handles all of the maintenance related operations.
    /// </summary>
    [Route("api/maintenance")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly ICacheStore _cacheStore;

        /// <summary>
        /// Constructor of the <see cref="MaintenanceController"/>
        /// </summary>
        /// <param name="cacheStore"></param>
        public MaintenanceController(ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
        }

        /// <summary>
        /// Clears the memory cache.
        /// </summary>
        /// <returns></returns>
        [Route("clear-memory-cache")]
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        public IActionResult ClearMemoryCache()
        {
            _cacheStore.Flush();
            return Ok("Memory Cache Cleared!");
        }
    }
}