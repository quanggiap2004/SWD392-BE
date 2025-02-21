namespace BlindBoxSystem.Domain.Model.OrderItem
{
    public class OrderItemSimpleDto
    {
        public int orderItemId { get; set; }

        public int boxOptionId { get; set; }

        public int quantity { get; set; }
        public float orderPrice { get; set; }
        public bool isFeedback { get; set; } = false;

        public string orderStatusCheckCardImage { get; set; }
        public bool isRefund { get; set; } = false;
        public bool openRequest { get; set; } = false;
    }
}
