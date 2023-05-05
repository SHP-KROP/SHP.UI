namespace SHP.OnlineShopAPI.Web.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public bool IsAvailable { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public bool IsLiked { get; set; }
    }
}
