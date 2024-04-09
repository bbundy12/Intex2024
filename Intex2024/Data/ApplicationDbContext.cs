using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intex2024.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserRecommendation> UserRecommendations { get; set; }
        public DbSet<ProductRecommendation> ProductRecommendations { get; set; }
        public DbSet<Cart> Carts { get; set; }

    }
}
