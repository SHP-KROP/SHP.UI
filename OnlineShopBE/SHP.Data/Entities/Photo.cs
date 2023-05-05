namespace DAL.Entities
{
    public class Photo
    {
        public int Id { get; set; }

        public string URL { get; set; }

        public bool IsMain { get; set; }

        public Product Product { get; set; }
    }
}
