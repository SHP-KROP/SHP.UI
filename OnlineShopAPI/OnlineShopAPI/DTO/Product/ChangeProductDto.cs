namespace OnlineShopAPI.DTO.Product
{
    public class ChangeProductDto
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public bool IsAvailable { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }
    }
}
