using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex2024.Data
{
    public class CustomerUser
    {
<<<<<<< HEAD
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public IdentityUser? Users { get; set; }
        public Customer? Customers { get; set; }
=======
        
        [Key]
        public string UserName { get; set; }
        public int CustomerId { get; set; } // This property will hold the foreign key value

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
>>>>>>> 3fbd3c8e12ed3689bd2133469965c7c484e88228
    }
}
