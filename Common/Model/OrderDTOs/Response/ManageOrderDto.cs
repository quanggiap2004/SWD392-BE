using Common.Model.Address.Response;
using Common.Model.OrderItem;
using Common.Model.OrderStatusDetailDTOs;
using Common.Model.VoucherDTOs.Response;

namespace Common.Model.OrderDTOs.Response
{
    public class ManageOrderDto
    {
        public int userId { get; set; }
        public int orderId { get; set; }
        public DateTime orderCreatedAt { get; set; }
        public string paymentMethod { get; set; }
        public decimal totalPrice { get; set; }
        public AddressResponseDto? address { get; set; }
        public bool openRequest { get; set; } = false;
        public bool refundRequest { get; set; } = false;
        public int currentStatusId { get; set; }
        public ICollection<OrderStatusDetailSimple> orderStatusDetailsSimple { get; set; }

        public ICollection<OrderItemSimpleDto> orderItems { get; set; }
        public VoucherDto? voucher { get; set; }
    }
}
