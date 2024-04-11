using Intex2024.Data;
using Microsoft.AspNetCore.Mvc;

namespace Intex2024.Components
{
    public class ProductColorsViewComponent : ViewComponent
    {
        private IIntexRepository _repo;
        public ProductColorsViewComponent(IIntexRepository temp){
            _repo = temp;
        } 

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedProductColor = RouteData?.Values["productColor"];

            var productColors = _repo.Products
                .Select(x => x.PrimaryColor)
                .Distinct()
                .OrderBy(x => x);

            return View(productColors);
        }
    }
}