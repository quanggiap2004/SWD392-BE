namespace Common.Model.OrderItem.Request
{
    public class OrderItemRequestDto
    {
        public int quantity { get; set; }
        public decimal price { get; set; }
        public int boxOptionId { get; set; }
        public decimal originPrice { get; set; }
        public bool isOnlineSerieBox { get; set; } = false;
        public int orderItemOpenRequestNumber { get; set; }
    }
}
