using System;
using System.ComponentModel.DataAnnotations;

namespace Intex2024.Data
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
    }

    public class CartLine
    {
        [Key]
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}