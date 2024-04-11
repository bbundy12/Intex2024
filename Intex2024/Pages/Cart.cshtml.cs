using Intex2024.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Intex2024.Pages
{
    public class CartModel : PageModel
    {
        private IIntexRepository _repo;
        public CartModel(IIntexRepository temp)
        {
            _repo = temp;
        }
        public Cart? Cart { get; set; }
        public void OnGet()
        {
            new Cart();
        }

        public void OnPost(int productId)
        {
            Product p = _repo.Products
                .FirstOrDefault(x => x.ProductId == productId);

            Cart = new Cart();
            
            Cart.AddItem(p, 1);
        }
    }
}