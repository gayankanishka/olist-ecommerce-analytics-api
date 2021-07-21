using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Location> Locations { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Seller> Sellers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
