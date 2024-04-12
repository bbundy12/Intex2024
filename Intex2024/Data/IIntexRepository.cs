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
        CustomerUser CustomerUsers { get; set; }
        public void AddProduct(Product ProductId);
        public void DeleteProduct(Product ProductId);
        public void UpdateProduct(Product ProductId);
       
        void SaveChanges();
        public void CreateAccount(Customer CustomerId);
        public void DeleteUser(Customer CustomerId);
        public void UpdateUser(Customer CustomerId);
        void UpdateProduct(Customer updatedInfo);

        void AddProduct(Customer customerId);
    }
}
