using System;

namespace Intex2024.Data
{
    public class Cart
    {
        public int CustomerId { get; set; }
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}