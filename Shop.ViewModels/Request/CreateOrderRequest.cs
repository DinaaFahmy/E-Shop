namespace Shop.ViewModels.Request
{
    public class CreateOrderRequest
    {
        public long CreatedBy { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
