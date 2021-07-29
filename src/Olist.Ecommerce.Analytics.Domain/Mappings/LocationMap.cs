using System.Globalization;
using CsvHelper.Configuration;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Domain.Mappings
{
    public sealed class LocationMap : ClassMap<Location>
    {
        public LocationMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Rank).Ignore();
        }
    }
}