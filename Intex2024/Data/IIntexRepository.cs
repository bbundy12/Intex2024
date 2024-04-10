using Microsoft.AspNetCore.Identity;

namespace Intex2024.Data
{
    public interface IIntexRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Customer> Customers { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Cart> Carts { get; }
        IQueryable<LineItem> LineItems { get; }
        IQueryable<UserRecommendation> UserRecommendations { get; }
        IQueryable<ProductRecommendation> ProductRecommendations { get; }
        IQueryable<IdentityRole> IdentityRoles { get; }
        void Update(Product updatedInfo);
        void SaveChanges();
        public IQueryable<Order> OrderNames();
    }
}
