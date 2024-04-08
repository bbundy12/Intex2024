using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Intex2024.Models
{
    public class UserRecommendation
    {
        [Key]
        public int RecommendationId { get; set;}
        public int UserId { get; set;}
        public int ProductId { get; set;}

        //Below will be the Users model
        [ForeignKey("UserId")]
        public IdentityUser User { get; set;}
        
        [ForeignKey("ProductId")]
        public Product Product { get; set;}
    }
}