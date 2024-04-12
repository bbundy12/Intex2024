using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

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
        public IQueryable<FraudPrediction> FraudPredictions { get; }
        public IQueryable<CustomerUser> CustomerUsers => _context.CustomerUsers;
        public IQueryable<Order> OrderNames()
        {
            var orderNames = _context.Orders
                .Include(o => o.Customer);
            
            return orderNames;
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

        public void SaveCustomer(Customer customer)
        {
            if (customer.CustomerId == 0)
            {
                // If the CustomerId is 0, it's a new customer, so add it to the context
                _context.Customers.Add(customer);
            }
            else
            {
                // If the CustomerId is not 0, then it's an existing customer, so update its data
                Customer dbEntry = _context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = customer.FirstName;
                    dbEntry.LastName = customer.LastName;
                    dbEntry.BirthDate = customer.BirthDate;
                    dbEntry.CountryOfResidence = customer.CountryOfResidence;
                    dbEntry.Gender = customer.Gender;
                    dbEntry.Age = customer.Age;
                    _context.Customers.Update(dbEntry);

                }
            }
            _context.SaveChanges();
        }

        public void UpdateCustomerUser(string username, int userId)
        {
            var customerUser = new CustomerUser
            {
                CustomerId = userId,
                UserName = username
                // Initialize other properties as necessary
            };
           // _context.CustomerUsers.Add(customerUser);
            _context.SaveChanges();
        }

        public void DeleteUser(Customer CustomerId)
        {
            _context.Customers.Remove(CustomerId);
            _context.SaveChanges();
        }

        public void UpdateUser(Customer CustomerId)
        {
            throw new NotImplementedException();
        }


        public void UpdateUser(CustomerUser id)
        {
           // _context.CustomerUsers.Update(id);
            _context.SaveChanges();
        }


        public void AddProduct(Customer customerId)
        {
            _context.Customers.Add(customerId);
            _context.SaveChanges();
            
        }
    
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
        public void CreateAccount(Customer id)
        {
            _context.Add(id);
            _context.SaveChanges();
        }
        public void UpdateProduct(Customer updatedInfo)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

    }
}
