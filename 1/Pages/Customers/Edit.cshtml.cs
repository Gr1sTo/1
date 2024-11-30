using _1.Data;
using _1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _context.Customers.FindAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingCustomer = await _context.Customers.FindAsync(Customer.Id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.FullName = Customer.FullName;
            existingCustomer.PhoneNumber = Customer.PhoneNumber;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}