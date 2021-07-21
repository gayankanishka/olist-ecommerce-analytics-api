using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    [Route("api/sellers")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        [HttpGet]
        [Route("most-popular/{itemId}")]
        public async Task<IActionResult> GetMostPopularSellersAsync([FromRoute] string itemId)
        {
            // TODO: This should return the most popular seller objects by itemId

            return Ok();
        }
    }
}
