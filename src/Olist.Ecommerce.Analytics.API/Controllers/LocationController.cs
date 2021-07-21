using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetLocationsAsync([FromQuery] string limit = "100")
        {
            // TODO: This should return all location objects

            return Ok();
        }

        [HttpGet]
        [Route("most-profitable")]
        public async Task<IActionResult> GetMostProfitableLocationsAsync()
        {
            // TODO: This should return a list of most profitable location objects

            return Ok();
        }

        [HttpGet]
        [Route("least-profitable-locations")]
        public async Task<IActionResult> GetLeastProfitableLocationsAsync()
        {
            // TODO: This should be a list of location objects with the item object

            return Ok();
        }
    }
}
