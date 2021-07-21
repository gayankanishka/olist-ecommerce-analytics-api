using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Olist.Ecommerce.Analytics.API.Controllers
{
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // TODO: What are the endpoints?

        // 1) Most profitable locations list
        // This should be a list of location objects 
        // /api/analytics/mostprofitablelocations

        // 2) Least profitable locations list with its most selling item
        // This should be a list of location objects with the item object
        // /api/analytics/leastprofitablelocations

        // 3) Get all items
        // This should return top 1000 items as objects
        // /api/items

        // 4) Get the most popular seller for a perticular item
        // This should return the sellers as objects
        // /api/analytics/mostpopularseller/{itemID}

        // 5) Get the slow moving items with the customers who have bought it
        // This should return the items as objects and users as objects alongside
        // /api/analytics/slowmovingitems/{locationID}

        // 6) Get all GEO locations
        // This should return a list of location objects
        // /api/analytics/geolocations

        // 7) Get sales percentages for item category level. This should support filter by weekly and daily.
        // This should return a list of categories with percentages
        // /api/analytics/salespercentages?type=weekly or /api/analytics/salespercentages?type=daily
    }
}
