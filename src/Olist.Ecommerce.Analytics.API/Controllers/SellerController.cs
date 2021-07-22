using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    [Route("api/sellers")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        [HttpGet]
        [Route("most-popular/{productId}")]
        public async Task<IActionResult> GetMostPopularSellersAsync([FromRoute] string productId)
        {
            // TODO: This should return the most popular seller objects by itemId

            if (string.IsNullOrWhiteSpace(productId))
            {
                return BadRequest("ProductId required!");
            }

            return Ok();
        }
    }
}
