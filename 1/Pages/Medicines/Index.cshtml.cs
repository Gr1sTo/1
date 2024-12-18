using _1.Data;
using _1.Models;
using _1.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace _1.Pages.Medicines
{
    [Authorize]
    public class MedicinesIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MedicinesIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Medicine> Medicines { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public async Task OnGetAsync()
        {
            IQueryable<Medicine> query = _context.Medicines;

            // Фільтрація
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(m => m.Name.Contains(SearchTerm) || m.Manufacturer.Contains(SearchTerm));
            }

            // Сортування
            query = SortOrder switch
            {
                "price_desc" => query.OrderByDescending(m => m.Price),
                "manufacturer_asc" => query.OrderBy(m => m.Manufacturer),
                "manufacturer_desc" => query.OrderByDescending(m => m.Manufacturer),
                _ => query.OrderBy(m => m.Name)
            };

            // Посторінкова навігація
            int pageSize = 3; // кількість записів на сторінку
            Medicines = await PaginatedList<Medicine>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}