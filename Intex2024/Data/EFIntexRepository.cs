﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Intex2024.Data
{
    public class EFIntexRepository : IIntexRepository
    {

        private ApplicationDbContext _context;
        public EFIntexRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Product> Products => _context.Products;
        public IQueryable<Customer> Customers => _context.Users;
        public IQueryable<Order> Orders => (IQueryable<Order>) _context.Orders;
        public IQueryable<Cart> Carts => _context.Carts;
        public IQueryable<LineItem> LineItems => _context.LineItems;
        public IQueryable<UserRecommendation> UserRecommendations => _context.UserRecommendations;
        public IQueryable<ProductRecommendation> ProductRecommendations => _context.ProductRecommendations;
        public IQueryable<IdentityRole> IdentityRoles => _context.Roles;

        public IQueryable<Order> OrderNames()
        {
            var orderNames = _context.Orders
                .Include(o => o.Customer);
            
            return orderNames;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product ProductId)
        {
            _context.Products.Add(ProductId);
            _context.SaveChanges();
        }
        public void DeleteProduct(Product ProductId)
        {
            _context.Products.Remove(ProductId);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product ProductId)
        {
            _context.Products.Update(ProductId);
            _context.SaveChanges();
        }
    }
}
