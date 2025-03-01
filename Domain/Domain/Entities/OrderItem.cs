using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }

        public int BoxOptionId { get; set; }

        public int Quantity { get; set; }
        public float OrderPrice { get; set; }
        public bool IsFeedback { get; set; } = false;

        public ICollection<string>? OrderStatusCheckCardImage { get; set; }
        public bool IsRefund { get; set; } = false;
        public int OpenRequestNumber { get; set; }
        public virtual Feedback Feedbacks { get; set; }
        [ForeignKey("BoxOptionId")]
        public BoxOption BoxOption { get; set; }
        public Order Order { get; set; }
        public int NumOfRefund { get; set; }

    }
}