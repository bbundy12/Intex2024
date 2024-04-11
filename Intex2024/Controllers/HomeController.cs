using Intex2024.Data;
using Intex2024.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;


namespace Intex2024.Controllers
{
    public class HomeController : Controller
    {
        private IIntexRepository _repo;
        private readonly UserManager<Customer> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IIntexRepository repo, UserManager<Customer> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Cart()
        {
            Cart cart = _httpContextAccessor.HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            Product product = _repo.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart cart = _httpContextAccessor.HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
                cart.AddItem(product, quantity);
                _httpContextAccessor.HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            Product product = _repo.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart cart = _httpContextAccessor.HttpContext.Session.GetJson<Cart>("Cart");
                if (cart != null)
                {
                    cart.RemoveLine(product);
                    _httpContextAccessor.HttpContext.Session.SetJson("Cart", cart);
                }
            }
            return RedirectToAction("Cart");
        } 

        public IActionResult Index()
        {
            var vm = new ProductsListViewModel
            {
                Products = _repo.Products
                .OrderBy(x => x.Name)
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Products(int pageNum)
        {
            int pageSize = 5;
            pageNum = Math.Max(1, pageNum); // Ensure pageNum is at least 1


            var vm = new ProductsListViewModel
            {
                Products = _repo.Products
                .OrderBy(x => x.Name)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Products.Count()
                }
            };
            return View(vm);
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }
        
        // [HttpPost]
        // Commented out the entire method as requested
        public IActionResult Orders()
        {
            var orders = _repo.Orders.ToList(); // Execute the query to retrieve the orders
            return View(orders);
        }




        public string? Customer { get; set; }

        public IActionResult AdminProducts()
        {
            var products = _repo.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Attempt to find the product by name
            Product recordToEdit = _repo.Products
                .Single(x => x.ProductId == id);
            // If a product was found, return the Edit view with the product data
            return View("AddProduct", recordToEdit);
        }

        
        [HttpPost]
        public IActionResult Edit(Product updatedInfo)
        {
            _repo.UpdateProduct(updatedInfo);

            return RedirectToAction("AdminProducts");
        }
        [HttpGet]
        public IActionResult DeleteConfirmation(int id)
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


        

        public IActionResult AdminUsers()
        {
            var customers = _repo.Customers.ToList();
            return View(customers);
        }
        

        public IActionResult Dashboard()
        {
            return View();
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
   


        public IActionResult Fraud()
        {
            return View();
        }
    }
}
