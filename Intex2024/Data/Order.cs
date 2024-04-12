using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Intex2024.Data.Cart;

namespace Intex2024.Data
{
    public class Order
    {
        [Key]
        public int TransactionId { get; set; }        
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter the day of week")]
        public string DayOfWeek { get; set; }
        [Required(ErrorMessage = "Please enter a Time")]
        public int Time { get; set; }
        [Required(ErrorMessage = "Please enter Card Type")]
        public string TypeOfCard { get; set; }
        [Required(ErrorMessage = "Please enter an Entry Mode")]
        public string EntryMode { get; set; }
        [Required(ErrorMessage = "Please enter an amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Please enter a TypeOfTransaction")]
        public string TypeOfTransaction { get; set; }
        [Required(ErrorMessage = "Please enter a country name")]
        public string CountryOfTransaction { get; set; }
        [Required(ErrorMessage = "Please enter a valid address")]
        public string ShippingAddress { get; set; }
        [Required(ErrorMessage = "Please enter a Bank")]
        public string Bank { get; set; }
        public bool Fraud { get; set; }
        public string CustomerId { get; set; } //Foreign Key to ASP.NET Users table
        public Customer Customer { get; set; }
        public Cart Cart { get; set; }
        public ICollection<LineItem> LineItems { get; set; }
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();
    }
}
