using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _1.Models;
using _1.Services;

namespace _1.Pages
{
    public class AddMedicineModel : PageModel
    {
        private readonly IMedicineService _medicineService;

        public AddMedicineModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [BindProperty]
        public Medicine NewMedicine { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _medicineService.AddMedicine(NewMedicine);

            return RedirectToPage("/Index");
        }
    }
}
