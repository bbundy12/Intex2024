using Intex2024.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intex2024.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IIntexRepository _repo;
        public AdminController(IIntexRepository repo)
        {
            _repo = repo;
        }
        
        public IActionResult Orders()
        {
            var orders = _repo.Orders.ToList(); // Execute the query to retrieve the orders
            return View(orders);
        }
        
      
        public IActionResult AdminProducts()
        {
            var products = _repo.Products.ToList();
            return View(products);
        }
       
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            // Attempt to find the product by id
            Product recordToEdit = _repo.Products
                .Single(x => x.ProductId == id);
            // If a product was found, return the Edit view with the product data
            return View("AddProduct", recordToEdit);
        }

       
        [HttpPost]
        public IActionResult EditProduct(Product updatedInfo)
        {
            _repo.UpdateProduct(updatedInfo);

            return RedirectToAction("AdminProducts");
        }
       
        [HttpGet]
        public IActionResult DeleteConfirmationProduct(int id)
        {
            var recordToDelete = _repo.Products
                .Single(x => x.ProductId == id);

            return View(recordToDelete); // Pass recordToDelete to the view
        }

        
        [HttpPost]
        public IActionResult DeleteConfirmationConfirmed(int productId)
        {
            var recordToDelete = _repo.Products
                .Single(x => x.ProductId == productId);

            _repo.DeleteProduct(recordToDelete); // Pass the entire Product object to the repository method

            return RedirectToAction("AdminProducts");
        }



        [HttpGet]
        public IActionResult AdminUsers()
        {
            var customers = _repo.Customers.ToList();
            return View(customers);
        }
        

        
        [HttpGet]
        public IActionResult AddProduct()
        {

            return View(new Product());
        }
        
        [HttpPost]
        public IActionResult AddProduct(Product response)
        {
            _repo.AddProduct(response); // Add product to database

            var products = _repo.Products.ToList();
            return View("AdminProducts", products);
        }
        [HttpGet]
        public IActionResult CreateAccount()
        {

            return View(new CustomerUser());
        }
        [HttpPost]
        public IActionResult CreateAccount(Customer id)
        {
            _repo.CreateAccount(id); // Add product to database

            var customers = _repo.Customers.ToList();
            return View("AdminUsers", customers);
        }

        [HttpGet]
        public IActionResult EditUsers(int id)
        {
            // Attempt to find the product by name
            CustomerUser recordToEdit = _repo.CustomerUsers;
                
            // If a product was found, return the Edit view with the product data
            return View("CreateAccount", recordToEdit);
        }
        

    }
}
