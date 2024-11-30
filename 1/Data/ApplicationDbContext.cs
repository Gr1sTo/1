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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
           
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Medicines)
                .WithMany()
                .UsingEntity(j => j.ToTable("OrderMedicines"));

            modelBuilder.Entity<Medicine>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18, 2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}