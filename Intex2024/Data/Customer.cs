using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Intex2024.Data
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
<<<<<<< HEAD
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CountryOfResidence { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public ICollection<Order>? Orders { get; set; }
=======
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string CountryOfResidence { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public ICollection<Order> Orders { get; set; }
>>>>>>> 3fbd3c8e12ed3689bd2133469965c7c484e88228
    }
}