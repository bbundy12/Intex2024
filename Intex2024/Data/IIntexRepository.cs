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
        public IQueryable<CustomerUser> CustomerUsers { get; }
        public void AddProduct(Product ProductId);
        public void DeleteProduct(Product ProductId);
        public void UpdateProduct(Product ProductId);
       
        void SaveChanges();
        public void SaveCustomer(Customer customer);
        public void UpdateCustomerUser(string username, int userId);
        public void CreateAccount(Customer CustomerId);
        public void DeleteUser(Customer CustomerId);

        void AddProduct(Customer customerId);
    }
}
