namespace _1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}