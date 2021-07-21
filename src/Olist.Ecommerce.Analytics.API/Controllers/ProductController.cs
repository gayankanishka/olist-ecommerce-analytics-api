using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromQuery] string limit = "100")
        {
            // TODO: This should return top 1000 items as objects

            return Ok();
        }

        [HttpGet]
        [Route("slow-moving/{locationId}")]
        public async Task<IActionResult> GetSlowMovingProductsAsync([FromRoute] string locationId)
        {
            // TODO: This should return the slow moving items as objects and users as objects alongside

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
