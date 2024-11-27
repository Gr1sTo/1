using _1.Models;

namespace _1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            //if (context.Medicines.Any())
            //{
            //    return;   
            //}

            var medicines = new Medicine[]
            {
                new Medicine { Name = "Aspirin", Manufacturer = "Bayer", Price = 10.5m, StockQuantity = 100 },
                new Medicine { Name = "Paracetamol", Manufacturer = "PharmaCo", Price = 8.25m, StockQuantity = 200 }
            };
            foreach (Medicine m in medicines)
            {
                context.Medicines.Add(m);
            }
            context.SaveChanges();
        }
    }
}
