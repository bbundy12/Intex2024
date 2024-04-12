using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex2024.Data
{
    public class ProductRecommendation
    {
<<<<<<< HEAD
        public int? RecommendedProductId { get; set; }
        public int? Rank { get; set; }
        public int? ProductId { get; set; }
        public decimal? SimilarityScore { get; set; }
=======
        public int RecommendedProductId { get; set; }
        public int Rank { get; set; }
        public int ProductId { get; set; }
        public int SimilarityScore { get; set; }
>>>>>>> 3fbd3c8e12ed3689bd2133469965c7c484e88228

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}