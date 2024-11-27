using _1.Models;
using Microsoft.EntityFrameworkCore;

namespace _1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}