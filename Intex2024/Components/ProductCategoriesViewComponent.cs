using Intex2024.Data;
using Microsoft.AspNetCore.Mvc;

namespace Intex2024.Components
{
    public class ProductCategoriesViewComponent : ViewComponent
    {
        private IIntexRepository _repo;
        public ProductCategoriesViewComponent(IIntexRepository temp){
            _repo = temp;
        } 

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedProductCategory = RouteData?.Values["productCategory"];

            var productCategories = _repo.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(productCategories);
        }
    }
}