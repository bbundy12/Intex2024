using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Intex2024.Data
{
    public class Customer : IdentityUser
    {
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string CountryOfResidence { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
