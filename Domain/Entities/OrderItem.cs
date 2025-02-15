using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlindBoxSystem.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }

        public int BoxVariantId { get; set; }

        public int Quantity { get; set; }
        public float OrderPrice { get; set; }
        public bool IsFeedback { get; set; } = false;
        
        public string OrderStatusCheckCardImage { get; set; }
        public bool IsRefund { get; set; } = false;
        public bool OpenRequest { get; set; } = false;
        public ICollection<Feedback> Feedback { get; set; }

        [ForeignKey("BoxVariantId")]
        public BoxVariant BoxVariant { get; set; }
        public Order Order { get; set; }

    }
}