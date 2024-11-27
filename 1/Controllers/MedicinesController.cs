using _1.Models;
using _1.Services;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var medicines = await _medicineService.GetAllMedicines();
            return Ok(medicines);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _medicineService.AddMedicine(medicine);
            return CreatedAtAction(nameof(GetAll), new { id = medicine.Id }, medicine);
        }
    }
}
