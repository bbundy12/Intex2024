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

        public virtual void AddItem(Product p, int quantity)
        {
            CartLine? line = Lines
                .Where(x => x.Product.ProductId == p.ProductId)
                .FirstOrDefault(); //find the first one that matches

            //has the item already been added to the cart?
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = p,
                    Quantity = quantity
                }); 
            }
            else
            {
                line.Quantity += quantity;
            }
            
        }

        public virtual void RemoveLine(Product p) =>
            Lines.RemoveAll(x => x.Product.ProductId == p.ProductId);
        public virtual void Clear() => Lines.Clear();
        public decimal CalculateTotal() =>
            Lines.Sum(x => 25 * x.Quantity);  
    }

    public class CartLine
    {
        [Key]
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}