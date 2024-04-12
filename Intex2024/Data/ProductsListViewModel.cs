namespace Intex2024.Data
{
    public class ProductsListViewModel
    {
        public IQueryable<Product>? Products { get; set; }

        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
        public IQueryable<UserRecommendation>  UserRecommendations{ get; set; }
        public string? CurrentProductCategory { get; set; }
        public string? CurrentProductColor { get; set; }
    }
}