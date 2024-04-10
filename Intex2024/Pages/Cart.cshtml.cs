using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Intex2024.Infrastructure;
using Intex2024.Data;

namespace Intex2024.Pages
{
    public class CartModel : PageModel
    {
        private IIntexRepository _repo;

        public Cart Cart { get; set; }
        public CartModel(IIntexRepository temp, Cart cartService)
        {
            _repo = temp;
            Cart = cartService;
        }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet()
        {
            ReturnUrl = ReturnUrl ?? "/";
            /*Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();*/
        }

        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product proj = _repo.Products
                .FirstOrDefault(x => x.ProductId == productId);

            // using the session
            if (proj != null)
            {
                /*Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();*/
                Cart.AddItem(proj, 1);
                /*HttpContext.Session.SetJson("cart", Cart);*/
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductId == productId).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }   
    }
}