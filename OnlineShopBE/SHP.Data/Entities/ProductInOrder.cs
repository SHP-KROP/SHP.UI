namespace DAL.Entities
{
    /// <summary>
    /// Represents an occasion of a product in an order
    /// </summary>
    public class ProductInOrder
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Amount { get; set; }
    }
}