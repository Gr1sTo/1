using _1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.Pages
{
    [Authorize]
    public class DatabaseViewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DatabaseViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int MedicinesCount { get; set; }
        public int CustomersCount { get; set; }
        public int OrdersCount { get; set; }

        public void OnGet()
        {
            MedicinesCount = _context.Medicines.Count();
            CustomersCount = _context.Customers.Count();
            OrdersCount = _context.Orders.Count();
        }
    }
}
