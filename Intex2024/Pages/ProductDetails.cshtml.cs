using Intex2024.Data;
using Intex2024.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;

namespace Intex2024.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IIntexRepository _repo;
        public ProductDetailsModel(IIntexRepository temp)
        {
            _repo = temp;
        }
        public Product? product { get; set; }
        public void OnGet()
        {
            
        }
        public void OnPost(int productId)
        {
            Product p = _repo.Products
                    .FirstOrDefault(x => x.ProductId == productId);
        }
        
    }
}