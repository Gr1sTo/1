using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _1.Data;
using _1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace _1.Pages.Medicines
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Medicine Medicine { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Medicine = await _context.Medicines.FindAsync(id);

            if (Medicine == null)
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

            var existingMedicine = await _context.Medicines.FindAsync(Medicine.Id);

            if (existingMedicine == null)
            {
                return NotFound(); // Якщо запис із цим ID не знайдено, повертається 404
            }

            existingMedicine.Name = Medicine.Name;
            existingMedicine.Manufacturer = Medicine.Manufacturer;
            existingMedicine.Price = Medicine.Price;
            existingMedicine.StockQuantity = Medicine.StockQuantity;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.Id == id);
        }
    }
}