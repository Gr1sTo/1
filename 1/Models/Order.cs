namespace _1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<Medicine> Medicines { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
