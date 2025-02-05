using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlindBoxSystem.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        
        public DateTime OrderCreatedAt { get; set; }
        public int VoucherId { get; set; }
        public string PaymentMethod { get; set; }
        public float TotalPrice { get; set; }
        public bool IsRefund { get; set; } = false;
        public float Revenue { get; set; }
        public int AddressId { get; set; }

        public virtual ICollection<OrderStatusDetail> OrderStatusDetails { get; set; }

        public virtual ICollection<OrderStatus> OrderStatus { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VoucherId")]
        public virtual Voucher Voucher { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Address Address { get; set; }

    }
}