using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public IQueryable<Customer> Customers => _context.Customers;
        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<Cart> Carts => _context.Carts;
        public IQueryable<LineItem> LineItems => _context.LineItems;
        public IQueryable<UserRecommendation> UserRecommendations => _context.UserRecommendations;
        public IQueryable<ProductRecommendation> ProductRecommendations => _context.ProductRecommendations;
        
        public object CustomerUsers { get; set; }

        public IQueryable<CustomerUser> CustomerUser => _context.CustomerUsers;

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

        public void CreateAccount(Customer id)
        {
            _context.Add(id);
            _context.SaveChanges();
        }

        public void DeleteUser(Customer CustomerId)
        {
            _context.Customers.Remove(CustomerId);
            _context.SaveChanges();
        }

        public void UpdateUser(Customer CustomerId)
        {
            _context.Customers.Update(CustomerId);
            _context.SaveChanges();
        }

        public void UpdateProduct(Customer updatedInfo)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Customer customerId)
        {
            _context.Customers.Add(CustomerId);
            _context.SaveChanges();
            
        }

        public Customer CustomerId { get; set; }
    }
}
