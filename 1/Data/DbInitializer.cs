using _1.Models;

namespace _1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Додавання ліків
            if (!context.Medicines.Any())
            {
                var medicines = new Medicine[]
                {
                    new Medicine { Name = "Aspirin", Manufacturer = "Bayer", Price = 10.5m, StockQuantity = 100 },
                    new Medicine { Name = "Paracetamol", Manufacturer = "PharmaCo", Price = 8.25m, StockQuantity = 200 },
                    new Medicine { Name = "Ibuprofen", Manufacturer = "Medico", Price = 12.75m, StockQuantity = 150 }
                };
                context.Medicines.AddRange(medicines);
                context.SaveChanges();
            }

            // Додавання клієнтів
            if (!context.Customers.Any())
            {
                var customers = new Customer[]
                {
                    new Customer { FullName = "John Doe", PhoneNumber = "+123456789" },
                    new Customer { FullName = "Jane Smith", PhoneNumber = "+987654321" }
                };
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            // Додавання замовлень
            if (!context.Orders.Any())
            {
                var john = context.Customers.FirstOrDefault(c => c.FullName == "John Doe");
                var jane = context.Customers.FirstOrDefault(c => c.FullName == "Jane Smith");

                var aspirin = context.Medicines.FirstOrDefault(m => m.Name == "Aspirin");
                var paracetamol = context.Medicines.FirstOrDefault(m => m.Name == "Paracetamol");
                var ibuprofen = context.Medicines.FirstOrDefault(m => m.Name == "Ibuprofen");

                if (john != null && aspirin != null && paracetamol != null)
                {
                    var order1 = new Order
                    {
                        CustomerId = john.Id,
                        OrderDate = DateTime.Now.AddDays(-5),
                        Medicines = new List<Medicine> { aspirin, paracetamol }
                    };
                    context.Orders.Add(order1);
                }

                if (jane != null && ibuprofen != null)
                {
                    var order2 = new Order
                    {
                        CustomerId = jane.Id,
                        OrderDate = DateTime.Now.AddDays(-2),
                        Medicines = new List<Medicine> { ibuprofen }
                    };
                    context.Orders.Add(order2);
                }

                context.SaveChanges();
            }
        }
    }
}
