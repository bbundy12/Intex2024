using Intex2024.Data;
using Intex2024.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace Intex2024.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IIntexRepository _repo;
        public ProductDetailsModel(IIntexRepository temp)
        {
            _repo = temp;
        }
        public Product product { get; set; }
        public List<Product> RecommendedProducts { get; set; }

        public void OnGet(int id)
        {
            product = _repo.Products
                    .FirstOrDefault(x => x.ProductId == id);

            if (product != null)
            {
                // Get all RecommendedProductIds for the clicked product
                var recommendedProductIds = _repo.ProductRecommendations
                                            .Where(pr => pr.ProductId == id)
                                            .Select(pr => pr.RecommendedProductId)
                                            .ToList();

                if (recommendedProductIds.Any())
                {
                    // Fetch all recommended products
                    RecommendedProducts = _repo.Products
                                            .Where(p => recommendedProductIds.Contains(p.ProductId))
                                            .ToList();
                }
            }
        }
        public void OnPost()
        {
            
        }

}
}