namespace BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs
{
    public class OrderStatusDetailSimple
    {
        public int orderId { get; set; }
        public int statusId { get; set; }
        public string statusName { get; set; }
        public string? note { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
