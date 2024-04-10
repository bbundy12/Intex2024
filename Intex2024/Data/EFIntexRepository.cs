using Microsoft.AspNetCore.Identity;

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
        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<Cart> Carts => _context.Carts;
        public IQueryable<LineItem> LineItems => _context.LineItems;
        public IQueryable<UserRecommendation> UserRecommendations => _context.UserRecommendations;
        public IQueryable<ProductRecommendation> ProductRecommendations => _context.ProductRecommendations;
        public IQueryable<IdentityRole> IdentityRoles => _context.Roles;
        public Customer GetCustomerData()
        {
            return _context.Users.FirstOrDefault();
        }
    }
}
