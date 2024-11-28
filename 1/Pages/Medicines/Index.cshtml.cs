using _1.Data;
using _1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.Pages.Medicines
{
    public class MedicinesIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MedicinesIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Medicine> Medicines { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Medicines = await _context.Medicines.ToListAsync();
        }
    }
}