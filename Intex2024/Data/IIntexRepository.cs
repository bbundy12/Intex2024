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
        IEnumerable<CustomerUser> CustomerUser { get; }

        public void AddProduct(Product ProductId);
        public void DeleteProduct(Product ProductId);
        public void UpdateProduct(Product ProductId);
        public IQueryable<Order> OrderNames();
        void SaveChanges();
    }
}
