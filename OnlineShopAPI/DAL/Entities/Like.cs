namespace DAL.Entities
{
    public class Like
    {
        public int Id { get; set; }

        public AppUser User { get; set; }

        public Product Product { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }
    }
}
