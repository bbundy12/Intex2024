using System;
using System.ComponentModel.DataAnnotations;

namespace Intex2024.Data
{
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int NumParts { get; set; }
        public decimal Price { get; set; }
        public string ImgLink { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
