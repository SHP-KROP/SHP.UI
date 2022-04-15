namespace DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Description { get; set; }
    }
}