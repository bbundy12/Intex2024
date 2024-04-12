using Intex2024.Data;
using Intex2024.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Diagnostics;
using System.Xml.Linq;
using Intex2024.Areas.Identity.Pages.Account;


namespace Intex2024.Controllers
{
    public class HomeController : Controller
    {
        private IIntexRepository _repo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly InferenceSession _session;
        private readonly string _onnxModelPath;
      
        public HomeController(IIntexRepository repo, UserManager<IdentityUser> userManager, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            string onnxModelPath = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "wwwroot/decision_tree_clf.onnx");
            _session = new InferenceSession(onnxModelPath);
        }

        /*[HttpPost]
        public IActionResult SubmitCart(CartSubmissionViewModel cartSubmission)
        {

            var predictions = new FraudPrediction();

            // Dictionary mapping the numeric prediction to a string
            var class_type_dict = new Dictionary<int, string>
        {
            { 0, "Not Fraud" },
            { 1, "Fraud" }
        };

            // Assuming you have all the necessary properties on your cartSubmission object, such as Age, Gender, etc.
            var input = new List<float>
    {
        cartSubmission.Customer.Age, // Assuming Age is already a float or can be cast to one.
        (float)cartSubmission.Order.Time, 
        (float)cartSubmission.Order.Amount, 

        // Country of residence
        cartSubmission.Customer.CountryOfResidence == "USA" ? 1 : 0,
        cartSubmission.Customer.CountryOfResidence == "United Kingdom" ? 1 : 0,

        // Gender
        cartSubmission.Customer.Gender == "M" ? 1 : 0,

        // Day of the week
        cartSubmission.Order.DayOfWeek == "Mon" ? 1 : 0,
        cartSubmission.Order.DayOfWeek == "Sat" ? 1 : 0,
        cartSubmission.Order.DayOfWeek == "Sun" ? 1 : 0,
        cartSubmission.Order.DayOfWeek == "Thu" ? 1 : 0,
        cartSubmission.Order.DayOfWeek == "Tue" ? 1 : 0,
        cartSubmission.Order.DayOfWeek == "Wed" ? 1 : 0,

        // Entry mode
        cartSubmission.Order.EntryMode == "PIN" ? 1 : 0,
        cartSubmission.Order.EntryMode == "Tap" ? 1 : 0,

        // Type of transaction
        cartSubmission.Order.TypeOfTransaction == "Online" ? 1 : 0,
        cartSubmission.Order.TypeOfTransaction == "POS" ? 1 : 0,

        // Country of transaction
        cartSubmission.Order.CountryOfTransaction == "India" ? 1 : 0,
        cartSubmission.Order.CountryOfTransaction == "Russia" ? 1 : 0,
        cartSubmission.Order.CountryOfTransaction == "USA" ? 1 : 0,
        cartSubmission.Order.CountryOfTransaction == "United Kingdom" ? 1 : 0,

        // Shipping address
        cartSubmission.Order.ShippingAddress == "India" ? 1 : 0,
        cartSubmission.Order.ShippingAddress == "Russia" ? 1 : 0,
        cartSubmission.Order.ShippingAddress == "USA" ? 1 : 0,
        cartSubmission.Order.ShippingAddress == "United Kingdom" ? 1 : 0,

        // Bank
        cartSubmission.Order.Bank == "HSBC" ? 1 : 0,
        cartSubmission.Order.Bank == "Halifax" ? 1 : 0,
        cartSubmission.Order.Bank == "Lloyds" ? 1 : 0,
        cartSubmission.Order.Bank == "Metro" ? 1 : 0,
        cartSubmission.Order.Bank == "Monzo" ? 1 : 0,
        cartSubmission.Order.Bank == "RBS" ? 1 : 0,

        // Type of card
        cartSubmission.Order.TypeOfCard == "Visa" ? 1 : 0,
    };

            var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

            var inputs = new List<NamedOnnxValue>
    {
        NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
    };

            string predictionResult;
            using (var results = _session.Run(inputs))
            {
                var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                predictionResult = prediction is not null && prediction.Length > 0
                    ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown")
                    : "Error in prediction";
            }

            bool isFraud = predictionResult == "Fraud";
            *//*SaveOrder(cartSubmission.Order, isFraud);*//*

            if (isFraud)
            {
                return View("ConfirmationPending");
            }
            else
            {
                return View("ConfirmationSuccess");
            }
        }*/

        public async Task<IActionResult> IndexAsync()
        {
            var vm = new ProductsListViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                vm.Products = _repo.UserRecommendations
                                .Where(ur => ur.UserId == 3) // Hardcoded UserId
                                .Select(ur => ur.Product);
            }
            else
            {
                vm.Products = _repo.Products.OrderBy(x => x.Name);
            }

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

        public IActionResult Products(int pageNum, int pageSize, string? productCategory, string? productColor)
        {
            pageNum = Math.Max(1, pageNum); // Ensure pageNum is at least 1
            pageSize = Math.Max(5, pageSize); // Ensure pageSize is at least 5

            var query = _repo.Products.AsQueryable();

            // Apply filters for productCategory and productColor
            if (!string.IsNullOrEmpty(productCategory))
            {
                query = query.Where(x => x.Category == productCategory);
            }
            if (!string.IsNullOrEmpty(productColor))
            {
                query = query.Where(x => x.PrimaryColor == productColor);
            }

            var vm = new ProductsListViewModel
            {
                Products = query
                    .OrderBy(x => x.Name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = query.Count() // Count after applying filters
                },

                CurrentProductCategory = productCategory,
                CurrentProductColor = productColor // Add the current product color to the view model
            };

            return View(vm);
        }

        public IActionResult ProductDetails()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CustomerInfo(string email)
        {
           
            var viewModel = new CustomerUserViewModel
            {
                Customer = new Customer(),
                UserName = email 
            };


            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CustomerInfo(CustomerUserViewModel model)
        {
                
            _repo.SaveCustomer(model.Customer);
            _repo.UpdateCustomerUser(model.UserName, model.Customer.CustomerId);

            return RedirectToAction("Index");
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }
        
         


        public IActionResult Fraud()
        {
            return View();
        }
    }
}
