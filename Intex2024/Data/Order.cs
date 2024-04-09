using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex2024.Data
{
    public class Order
    {
        [Key]
        public int TransactionId { get; set; }
        public int CustomerId { get; set; } //Foreign Key to ASP.NET Users table
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan Time { get; set; }
        public string TypeOfCard { get; set; }
        public string EntryMode { get; set; }
        public decimal Amount { get; set; }
        public string TypeOfTransaction { get; set; }
        public string CountryOfTransaction { get; set; }
        public string ShippingAddress { get; set; }
        public string Bank { get; set; }
        public bool Fraud { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public ICollection<LineItem> LineItems { get; set; }
    }
}
