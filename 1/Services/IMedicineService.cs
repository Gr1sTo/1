using _1.Models;

namespace _1.Services
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetAllMedicines();
        Task<Medicine> GetMedicineById(int id);
        Task AddMedicine(Medicine medicine);
    }
}
