namespace OnlineShopAPI.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public bool IsAvaliable { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }
    }
}
