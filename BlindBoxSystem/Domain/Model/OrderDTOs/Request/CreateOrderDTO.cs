using BlindBoxSystem.Domain.Model.OrderItem.Request;

namespace BlindBoxSystem.Domain.Model.OrderDTOs.Request
{
    public class CreateOrderDTO
    {
        public int userId { get; set; }
        public float totalPrice { get; set; }
        public int voucherId { get; set; }
        public string paymentMethod { get; set; }
        public int addressId { get; set; }
        public ICollection<OrderItemRequestDto> orderItemRequestDto { get; set; }
    }
}
