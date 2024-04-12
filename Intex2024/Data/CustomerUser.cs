using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex2024.Data
{
    public class CustomerUser
    {
        
        [Key]
        public string UserName { get; set; }
        public int CustomerId { get; set; } // This property will hold the foreign key value

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
