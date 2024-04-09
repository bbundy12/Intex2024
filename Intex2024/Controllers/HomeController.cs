using Intex2024.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            return View();
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
    }
}
