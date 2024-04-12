using Intex2024.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex2024.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IIntexRepository _repo;
        private readonly InferenceSession _session;
        private readonly string _onnxModelPath;
        public AdminController(IIntexRepository repo, IHostEnvironment hostEnvironment)
        {
            _repo = repo;

            _onnxModelPath = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "decision_tree_clf.onnx");

            _session = new InferenceSession(_onnxModelPath);
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
        
      public IActionResult OrderPredictions()
{
    var records = _repo.Orders
        .Include(o => o.Customer) // Include the Customer navigation property
        .OrderByDescending(o => o.Date)
        .Take(20)
        .ToList();  // Fetch the 20 most recent records with their corresponding customers
    
    var predictions = new List<FraudPrediction>();  // Your ViewModel for the view

    // Dictionary mapping the numeric prediction to an animal type
    var class_type_dict = new Dictionary<int, string>
        {
            { 0, "Not Fraud" },
            { 1, "Fraud" }
        };

    foreach (var cartSubmission in records)
    {
        // Preprocess features to make them compatible with the model
        var input = new List<float>
            {
        cartSubmission.Customer.Age, // Assuming Age is already a float or can be cast to one.
        (float)cartSubmission.Time, 
        (float)cartSubmission.Amount, 

        // Country of residence
        cartSubmission.Customer.CountryOfResidence == "USA" ? 1 : 0,
        cartSubmission.Customer.CountryOfResidence == "United Kingdom" ? 1 : 0,

        // Gender
        cartSubmission.Customer.Gender == "M" ? 1 : 0,

        // Day of the week
        cartSubmission.DayOfWeek == "Mon" ? 1 : 0,
        cartSubmission.DayOfWeek == "Sat" ? 1 : 0,
        cartSubmission.DayOfWeek == "Sun" ? 1 : 0,
        cartSubmission.DayOfWeek == "Thu" ? 1 : 0,
        cartSubmission.DayOfWeek == "Tue" ? 1 : 0,
        cartSubmission.DayOfWeek == "Wed" ? 1 : 0,

        // Entry mode
        cartSubmission.EntryMode == "PIN" ? 1 : 0,
        cartSubmission.EntryMode == "Tap" ? 1 : 0,

        // Type of transaction
        cartSubmission.TypeOfTransaction == "Online" ? 1 : 0,
        cartSubmission.TypeOfTransaction == "POS" ? 1 : 0,

        // Country of transaction
        cartSubmission.CountryOfTransaction == "India" ? 1 : 0,
        cartSubmission.CountryOfTransaction == "Russia" ? 1 : 0,
        cartSubmission.CountryOfTransaction == "USA" ? 1 : 0,
        cartSubmission.CountryOfTransaction == "United Kingdom" ? 1 : 0,

        // Shipping address
        cartSubmission.ShippingAddress == "India" ? 1 : 0,
        cartSubmission.ShippingAddress == "Russia" ? 1 : 0,
        cartSubmission.ShippingAddress == "USA" ? 1 : 0,
        cartSubmission.ShippingAddress == "United Kingdom" ? 1 : 0,

        // Bank
        cartSubmission.Bank == "HSBC" ? 1 : 0,
        cartSubmission.Bank == "Halifax" ? 1 : 0,
        cartSubmission.Bank == "Lloyds" ? 1 : 0,
        cartSubmission.Bank == "Metro" ? 1 : 0,
        cartSubmission.Bank == "Monzo" ? 1 : 0,
        cartSubmission.Bank == "RBS" ? 1 : 0,

        // Type of card
        cartSubmission.TypeOfCard == "Visa" ? 1 : 0,
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
            predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
        }

        predictions.Add(new FraudPrediction() { Order = cartSubmission, Prediction = predictionResult }); // Adds the animal information and prediction for that animal to AnimalPrediction viewmodel
    }

    return View(predictions);
}

        

    }
}
