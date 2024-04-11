using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        // Set the column type to VARCHAR with a max length of 255
                        property.SetColumnType("VARCHAR(255)");
                    }
                }
            }
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LineItem>()
                .HasKey(e => new { e.ProductId, e.TransactionId });

            modelBuilder.Entity<ProductRecommendation>()
                .HasKey(e => new { e.RecommendedProductId, e.Rank, e.ProductId });

            modelBuilder.Entity<UserRecommendation>()
                .HasKey(e => new { e.UserId, e.RecommendationId });
        }
        public DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public DbSet<UserRecommendation> UserRecommendations { get; set; }
        public DbSet<ProductRecommendation> ProductRecommendations { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
