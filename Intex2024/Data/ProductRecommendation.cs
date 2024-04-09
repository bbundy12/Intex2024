using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex2024.Data
{
    public class ProductRecommendation
    {
        public int RecommendedProductId { get; set; }
        public int Rank { get; set; }
        public int ProductId { get; set; }
        public decimal SimilarityScore { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}