using Microsoft.AspNetCore.Identity;

namespace Intex2024.Data
{
    public class CustomerUser
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public IdentityUser? Users { get; set; }
        public Customer? Customers { get; set; }
    }
}
