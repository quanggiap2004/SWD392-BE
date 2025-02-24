
using BlindBoxSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs;
using BlindBoxSystem.Domain.Model.OrderItem;
using BlindBoxSystem.Domain.Model.VoucherDTOs.Response;
using BlindBoxSystem.Domain.Model.Address.Response;

namespace BlindBoxSystem.Domain.Model.OrderDTOs.Response
{
    public class ManageOrderDto
    {
        public int userId { get; set; }
        public int orderId { get; set; }
        public DateTime orderCreatedAt { get; set; }
        public string paymentMethod { get; set; }
        public float totalPrice { get; set; }
        public AddressResponseDto address { get; set; }
        public bool openRequest { get; set; } = false;
        public bool refundRequest { get; set; } = false;
        public int currentStatusId { get; set; }
        public ICollection<OrderStatusDetailSimple> orderStatusDetailsSimple { get; set; }

        public ICollection<OrderItemSimpleDto> orderItems { get; set; }
        public VoucherDto voucher { get; set; }
    }
}
