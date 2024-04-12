using Intex2024.Data;
using Intex2024.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Diagnostics;
using System.Xml.Linq;


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
// Beginning of commented code
    //     [HttpPost]
    //     public IActionResult SubmitCart(CartSubmissionViewModel cartSubmission)
    //     {

    //         var predictions = new FraudPrediction();

    //         // Dictionary mapping the numeric prediction to a string
    //         var class_type_dict = new Dictionary<int, string>
    //     {
    //         { 0, "Not Fraud" },
    //         { 1, "Fraud" }
    //     };

    //         // Assuming you have all the necessary properties on your cartSubmission object, such as Age, Gender, etc.
    //         var input = new List<float>
    // {
    //     cartSubmission.Customer.Age, // Assuming Age is already a float or can be cast to one.
    //     (float)cartSubmission.Order.Time, 
    //     (float)cartSubmission.Order.Amount, 

    //     // Country of residence
    //     cartSubmission.Customer.CountryOfResidence == "USA" ? 1 : 0,
    //     cartSubmission.Customer.CountryOfResidence == "United Kingdom" ? 1 : 0,

    //     // Gender
    //     cartSubmission.Customer.Gender == "M" ? 1 : 0,

    //     // Day of the week
    //     cartSubmission.Order.DayOfWeek == "Mon" ? 1 : 0,
    //     cartSubmission.Order.DayOfWeek == "Sat" ? 1 : 0,
    //     cartSubmission.Order.DayOfWeek == "Sun" ? 1 : 0,
    //     cartSubmission.Order.DayOfWeek == "Thu" ? 1 : 0,
    //     cartSubmission.Order.DayOfWeek == "Tue" ? 1 : 0,
    //     cartSubmission.Order.DayOfWeek == "Wed" ? 1 : 0,

    //     // Entry mode
    //     cartSubmission.Order.EntryMode == "PIN" ? 1 : 0,
    //     cartSubmission.Order.EntryMode == "Tap" ? 1 : 0,

    //     // Type of transaction
    //     cartSubmission.Order.TypeOfTransaction == "Online" ? 1 : 0,
    //     cartSubmission.Order.TypeOfTransaction == "POS" ? 1 : 0,

    //     // Country of transaction
    //     cartSubmission.Order.CountryOfTransaction == "India" ? 1 : 0,
    //     cartSubmission.Order.CountryOfTransaction == "Russia" ? 1 : 0,
    //     cartSubmission.Order.CountryOfTransaction == "USA" ? 1 : 0,
    //     cartSubmission.Order.CountryOfTransaction == "United Kingdom" ? 1 : 0,

    //     // Shipping address
    //     cartSubmission.Order.ShippingAddress == "India" ? 1 : 0,
    //     cartSubmission.Order.ShippingAddress == "Russia" ? 1 : 0,
    //     cartSubmission.Order.ShippingAddress == "USA" ? 1 : 0,
    //     cartSubmission.Order.ShippingAddress == "United Kingdom" ? 1 : 0,

    //     // Bank
    //     cartSubmission.Order.Bank == "HSBC" ? 1 : 0,
    //     cartSubmission.Order.Bank == "Halifax" ? 1 : 0,
    //     cartSubmission.Order.Bank == "Lloyds" ? 1 : 0,
    //     cartSubmission.Order.Bank == "Metro" ? 1 : 0,
    //     cartSubmission.Order.Bank == "Monzo" ? 1 : 0,
    //     cartSubmission.Order.Bank == "RBS" ? 1 : 0,

    //     // Type of card
    //     cartSubmission.Order.TypeOfCard == "Visa" ? 1 : 0,
    // };

    //         var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

    //         var inputs = new List<NamedOnnxValue>
    // {
    //     NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
    // };

    //         string predictionResult;
    //         using (var results = _session.Run(inputs))
    //         {
    //             var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
    //             predictionResult = prediction is not null && prediction.Length > 0
    //                 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown")
    //                 : "Error in prediction";
    //         }

    //         bool isFraud = predictionResult == "Fraud";
    //         *//*SaveOrder(cartSubmission.Order, isFraud);*//*

    //         if (isFraud)
    //         {
    //             return View("ConfirmationPending");
    //         }
    //         else
    //         {
    //             return View("ConfirmationSuccess");
    //         }
    //     }
// End of commented code
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

        public IActionResult NotFraud()
        {
            return View();
        }
    }
}
