using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Intex2024.Data;
using Microsoft.Extensions.Azure;

namespace Intex2024.Controllers {

    public class OrderController : Controller {
        private IOrderRepository repository;
        private Cart cart;
                
        public OrderController(IOrderRepository repoService,
                Cart cartService) {
            repository = repoService;
            cart = cartService;
        }

        public ViewResult Checkout()
        {
            // Create a new Order instance
            var order = new Order();

            // Initialize the Cart property

            

            // Pass the order object to the Confirmation view
            return View("Confirmation", order);
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Assign day of week
                    order.DayOfWeek = DateTime.Now.DayOfWeek.ToString();

                    // Assign time
                    order.Time = DateTime.Now.Hour;

                    // Assign entry mode
                    order.EntryMode = "CVC";

                    // Assign type of transaction
                    order.TypeOfTransaction = "Online";

                    // Save the order to the database
                    repository.SaveOrder(order);

                    // Redirect to the confirmation view
                    return RedirectToAction("Fraud", "Home");
                }
                catch (Exception ex)
                {
                    // Handle exception, log error, etc.
                    ModelState.AddModelError("", "An error occurred while processing the payment.");
                }
            }
            // If ModelState is not valid, return to the payment view with errors
            return View("~/Views/Order/Confirmation.cshtml", order);
        }
    }
}