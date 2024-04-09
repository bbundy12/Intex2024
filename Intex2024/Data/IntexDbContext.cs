using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Intex2024.Data
{
    public class IntexDbContext : DbContext

    {
        public IntexDbContext(DbContextOptions<IntexDbContext> options) : base(options)
        {
        }
        /*public DbSet<Project> Projects { get; set; }*/
    }
}
