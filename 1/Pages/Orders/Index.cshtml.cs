using _1.Data;
using _1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.Pages.Orders
{
    [Authorize]
    public class OrdersIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrdersIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}