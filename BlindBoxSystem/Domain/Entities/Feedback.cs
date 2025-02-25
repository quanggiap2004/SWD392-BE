using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlindBoxSystem.Domain.Entities
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string FeedbackContent { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }


        public string FeedbackType { get; set; }
        public string ImageUrl { get; set; }

        public int OrderItemId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("OrderItemId")]
        public virtual OrderItem OrderItem { get; set; }
    }
}
