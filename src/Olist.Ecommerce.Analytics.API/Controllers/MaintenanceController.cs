using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    /// <summary>
    /// Handles all of the maintenance related operations.
    /// </summary>
    [Route("api/maintenance")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Constructor of the <see cref="MaintenanceController"/>
        /// </summary>
        /// <param name="memoryCache"></param>
        public MaintenanceController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Clears the memory cache.
        /// </summary>
        /// <returns></returns>
        [Route("clear-memory-cache")]
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public IActionResult ClearMemoryCacheAsync()
        {
            if (_memoryCache is not MemoryCache memoryCache)
            {
                return NotFound("Cache Not Found!");
            }
            
            double percentage = 1.0;
            memoryCache.Compact(percentage);
            
            return Ok("Memory Cache Cleared!");
        }
    }
}