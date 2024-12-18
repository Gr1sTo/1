using _1.Data;
using _1.Models;
using _1.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace _1.Pages.Customers
{
    [Authorize]
    public class CustomersIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Customer> Customers { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public async Task OnGetAsync()
        {
            IQueryable<Customer> query = _context.Customers;

            // Фільтрація
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(c => c.FullName.Contains(SearchTerm) || c.PhoneNumber.Contains(SearchTerm));
            }

            // Сортування
            query = SortOrder switch
            {
                "name_desc" => query.OrderByDescending(c => c.FullName),
                "phone_asc" => query.OrderBy(c => c.PhoneNumber),
                "phone_desc" => query.OrderByDescending(c => c.PhoneNumber),
                _ => query.OrderBy(c => c.FullName)
            };

            // Посторінкова навігація
            int pageSize = 5; // кількість записів на сторінку
            Customers = await PaginatedList<Customer>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}