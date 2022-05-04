namespace DAL.Entities
{
    public class Like
    {
        public AppUser User { get; set; }

        public Product Product { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }
    }
}
