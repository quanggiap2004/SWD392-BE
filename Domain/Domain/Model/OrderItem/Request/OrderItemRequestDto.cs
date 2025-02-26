namespace Domain.Domain.Model.OrderItem.Request
{
    public class OrderItemRequestDto
    {
        public int quantity { get; set; }
        public float price { get; set; }
        public int boxOptionId { get; set; }
        public int originPrice { get; set; }
        public int orderItemOpenRequestNumber { get; set; }
    }
}
