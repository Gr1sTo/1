using _1.Data;
using _1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = new Order();

        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
        public List<Customer> Customers { get; set; } = new List<Customer>();

        [BindProperty]
        public List<int> SelectedMedicines { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Завантаження доступних клієнтів і медикаментів
            Medicines = await _context.Medicines.ToListAsync();
            Customers = await _context.Customers.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPostAsync called.");

            if (Order.CustomerId <= 0)
            {
                ModelState.AddModelError(nameof(Order.CustomerId), "Please select a customer.");
            }

            if (!SelectedMedicines.Any())
            {
                ModelState.AddModelError(string.Empty, "At least one medicine must be selected.");
            }

            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                    }
                }
                Medicines = await _context.Medicines.ToListAsync();
                Customers = await _context.Customers.ToListAsync();
                return Page();
            }

            // Прив'язка медикаментів до замовлення
            Order.Medicines = await _context.Medicines
                .Where(m => SelectedMedicines.Contains(m.Id))
                .ToListAsync();

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            Console.WriteLine("Order saved successfully.");
            return RedirectToPage("/Orders/Index");
        }
    }
}