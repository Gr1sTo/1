using _1.Data;
using _1.Models;
using Microsoft.EntityFrameworkCore;

namespace _1.Services
{
    public class MedicineService: IMedicineService
    {
        private readonly ApplicationDbContext _context;

        public MedicineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medicine>> GetAllMedicines()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<Medicine> GetMedicineById(int id)
        {
            return await _context.Medicines.FindAsync(id);
        }

        public async Task AddMedicine(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
        }
    }
}
