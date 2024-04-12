using Microsoft.AspNetCore.Mvc;
using Intex2024.Data;

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
            order.Cart = new Cart();

            

            // Pass the order object to the Confirmation view
            return View("Confirmation", order);
        }

         [HttpPost]
        public IActionResult Checkout(Order order) {
            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("", 
                    "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid) {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/", 
                    new { orderId = order.TransactionId });
            } else {
                return View("Confirmation");
            }
        }
    }
}