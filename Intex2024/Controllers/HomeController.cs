using Intex2024.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;


namespace Intex2024.Controllers
{
    public class HomeController : Controller
    {
        private IIntexRepository _repo;
        public HomeController(IIntexRepository repo)
        {
            _repo = repo;
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

        public IActionResult Products()
        {
            return View();
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

        public IActionResult Cart()
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
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product recordToEdit = _repo.Products
                .Single(p => p.ProductId == id);
            
            return View("AddProduct", recordToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Product updatedInfo)
        {
            _repo.Update(updatedInfo);
            _repo.SaveChanges();

            return RedirectToAction("AdminProducts");
        }

        public IActionResult AdminUsers()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        /*
   [HttpGet]
   public IActionResult AddProduct(Product response)
   {
       _repo.Products
           .OrderBy(p => p.Name)
           .ToList();

       return View();
   }

   [HttpPost]
   public IActionResult AddProduct(Product response)
   {
       _repo.Products.Add(response); // Add product to database
       _repo.SaveChanges();
       return View(AdminProducts);  // Assuming AdminProducts is a variable or constant name
   }
   */


        public IActionResult Fraud()
        {
            return View();
        }
    }
}
