using Intex2024.Data;
using Intex2024.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;

namespace Intex2024.Pages
{
    [Authorize(Roles = "Customer")]
    public class CartModel : PageModel
    {
        private IIntexRepository _repo;
        public CartModel(IIntexRepository temp, Cart cartService)
        {
            _repo = temp;
            Cart = cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product p = _repo.Products
                .FirstOrDefault(x => x.ProductId == productId);

                if (p != null)
                {
                    Cart.AddItem(p, 1);
                }

                return RedirectToPage (new {returnUrl = returnUrl});

        }
        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(x => x.Product.ProductId == productId).Product);
            return RedirectToPage (new {returnUrl = returnUrl});
        }
    }
}