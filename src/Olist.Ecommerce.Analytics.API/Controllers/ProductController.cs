using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("fast-moving")]
        public async Task<IActionResult> GetFastMovingProductsAsync()
        {
            // TODO: This should return top selling products

            return Ok();
        }

        [HttpGet]
        [Route("slow-moving/{locationId}")]
        public async Task<IActionResult> GetSlowMovingProductsAsync([FromRoute] string locationId)
        {
            // TODO: This should return the slow moving items as objects and users as objects alongside

            if (string.IsNullOrWhiteSpace(locationId))
            {
                return BadRequest("LocationId required!");
            }

            return Ok();
        }

        [HttpGet]
        [Route("sales-percentages")]
        public async Task<IActionResult> GetSalesPercentagesAsync([FromQuery] string type)
        {
            // TODO: This should return a list of categories with percentages
            // /api/analytics/salespercentages?type=weekly or /api/analytics/salespercentages?type=daily

            return Ok();
        }
    }
}
