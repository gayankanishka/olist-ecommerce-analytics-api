using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Location> Locations { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Seller> Sellers { get; set; } = default!;
    }
}
