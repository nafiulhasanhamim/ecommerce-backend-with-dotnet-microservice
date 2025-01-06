using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }

    }
}