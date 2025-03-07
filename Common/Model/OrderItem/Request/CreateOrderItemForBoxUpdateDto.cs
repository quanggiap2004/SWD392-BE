namespace Common.Model.OrderItem.Request
{
    public class CreateOrderItemForBoxUpdateDto
    {
        int orderId { get; set; }
        int boxOptionId { get; set; }
        int quantity { get; set; }
        float price { get; set; }
        int orderItemOpenRequestNumber { get; set; }
        ICollection<string> OrderStatusCheckCardImage = new List<string>();
    }
}
