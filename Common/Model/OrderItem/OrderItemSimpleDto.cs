namespace Common.Model.OrderItem
{
    public class OrderItemSimpleDto
    {
        public int orderItemId { get; set; }

        public int boxOptionId { get; set; }

        public int quantity { get; set; }
        public decimal orderPrice { get; set; }
        public bool isFeedback { get; set; } = false;

        public ICollection<string> orderStatusCheckCardImage { get; set; }
        public bool isRefund { get; set; } = false;
        public int openRequestNumber { get; set; }
        public string boxOptionName { get; set; }
        public string boxName { get; set; }
        public string? imageUrl { get; set; }
        public int numOfRefund { get; set; }
    }
}
