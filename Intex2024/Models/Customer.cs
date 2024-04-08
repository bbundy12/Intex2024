using System;
using System.ComponentModel.DataAnnotations;

namespace Intex2024.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string CountryOfResidence { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
