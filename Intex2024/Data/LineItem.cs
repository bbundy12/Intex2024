using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex2024.Data
{
    public class LineItem
    {
        [Key]
        public int TransactionId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Rating { get; set; }

        [ForeignKey("TransactionId")]
        public Order Order { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
