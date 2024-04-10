﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intex2024.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LineItem>()
                .HasKey(e => new { e.ProductId, e.TransactionId });

            modelBuilder.Entity<ProductRecommendation>()
                .HasKey(e => new { e.RecommendedProductId, e.Rank });
        }
        public DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public DbSet<UserRecommendation> UserRecommendations { get; set; }
        public DbSet<ProductRecommendation> ProductRecommendations { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<LineItem> LineItems { get; set; }

    }
}
