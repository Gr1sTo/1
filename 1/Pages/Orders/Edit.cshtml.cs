using _1.Data;
using _1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Order = await _context.Orders.FindAsync(id);

            if (Order == null)
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

            var existingOrder = await _context.Orders.FindAsync(Order.Id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.CustomerId = Order.CustomerId;
            existingOrder.OrderDate = Order.OrderDate;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}