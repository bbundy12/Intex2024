using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Intex2024.Data
{
    public class EFIntexRepository : IIntexRepository
    {

        private IntexDbContext _context;
        public EFIntexRepository(IntexDbContext ctx)
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

    }
}
