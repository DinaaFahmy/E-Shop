namespace Shop.ViewModels.Response
{
    public class ProductResponse
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }

        public double? DiscountPercentage { get; set; }
        public double? PriceForTwoPieces { get; set; }
    }
}
