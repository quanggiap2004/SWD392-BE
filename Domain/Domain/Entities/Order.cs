using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public DateTime OrderCreatedAt { get; set; }
        public int? VoucherId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Revenue { get; set; }
        public int? AddressId { get; set; }
        public bool OpenRequest { get; set; } = false;
        public bool RefundRequest { get; set; } = false;
        public int CurrentOrderStatusId { get; set; }
        public string PaymentStatus { get; set; } = ProjectConstant.PaymentPending;
        public bool IsEnable { get; set; } = true;
        public decimal ShippingFee { get; set; }
        [Column(TypeName = "jsonb")]
        public string? JsonOrderModel { get; set; }
        public virtual ICollection<OrderStatusDetail> OrderStatusDetails { get; set; }

        public virtual ICollection<OrderStatus> OrderStatus { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VoucherId")]
        public virtual Voucher Voucher { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Address Address { get; set; }

    }
}