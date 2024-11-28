using System.ComponentModel.DataAnnotations;

namespace _1.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [Range(1, 10000)]
        public int StockQuantity { get; set; }
    }
}
