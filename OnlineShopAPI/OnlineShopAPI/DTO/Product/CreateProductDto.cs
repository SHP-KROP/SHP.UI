namespace OnlineShopAPI.DTO.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public bool IsAvaliable { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }
    }
}
