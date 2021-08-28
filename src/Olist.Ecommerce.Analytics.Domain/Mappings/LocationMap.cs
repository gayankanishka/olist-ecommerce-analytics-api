using System.Globalization;
using CsvHelper.Configuration;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Domain.Mappings
{
    /// <summary>
    /// Location mapping for CSV file.
    /// </summary>
    public sealed class LocationMap : ClassMap<Location>
    {
        /// <summary>
        /// Map location properties.
        /// </summary>
        public LocationMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Rank).Ignore();
        }
    }
}