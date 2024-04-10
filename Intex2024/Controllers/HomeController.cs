using Intex2024.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Intex2024.Controllers
{
    public class HomeController : Controller
    {
        private IIntexRepository _repo;
        private readonly UserManager<Customer> _userManager;

        public HomeController(IIntexRepository repo, UserManager<Customer> userManager)
        {
            _repo = repo;
            _userManager = userManager;
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

        public IActionResult Cart()
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

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult AdminProducts()
        {
            return View();
        }

        public IActionResult AdminUsers()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult Fraud()
        {
            return View();
        }

    }
}
