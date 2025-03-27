using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        [ForeignKey("UserRolledItem")]
        public int? UserRolledItemId { get; set; }
        public int OrderId { get; set; }

        public int BoxOptionId { get; set; }

        public int Quantity { get; set; }
        public decimal OrderPrice { get; set; }
        public bool IsFeedback { get; set; } = false;
        public string RefundStatus { get; set; } = ProjectConstant.RefundAvailable;

        public ICollection<string>? OrderStatusCheckCardImage { get; set; }
        public int OpenRequestNumber { get; set; }
        public string Note { get; set; } = "";
        public virtual Feedback Feedback { get; set; }
        [ForeignKey("BoxOptionId")]
        public BoxOption BoxOption { get; set; }
        public Order Order { get; set; }
        public virtual UserRolledItem UserRolledItem { get; set; }
        public int NumOfRefund { get; set; }
    }
}