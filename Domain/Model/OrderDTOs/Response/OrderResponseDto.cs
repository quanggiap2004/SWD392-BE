using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Domain.Model.OrderDTOs.Response
{
    public class OrderResponseDto
    {
        public int orderId { get; set; }
        public int userId { get; set; }
        public DateTime orderCreatedAt { get; set; }
        public string paymentMethod { get; set; }
        public float totalPrice { get; set; }
        public float revenue { get; set; }
        public int addressId { get; set; }
        public int currentOrderStatusId { get; set; }
        public string url { get; set; }
    }
}
