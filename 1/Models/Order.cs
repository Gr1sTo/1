using System.ComponentModel.DataAnnotations;

namespace _1.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } // Навігаційна властивість

        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
        public DateTime OrderDate { get; set; }
    }
}