using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class BoxOption
    {
        [Key]
        public int BoxOptionId { get; set; }  // Primary Key
        public int BoxId { get; set; }  // Foreign Key to Box

        [Required]
        public string BoxOptionName { get; set; }  // nvarchar(200)

        public decimal OriginPrice { get; set; }  // float

        public decimal DisplayPrice { get; set; }  // float

        public int BoxOptionStock { get; set; }  // int
        public bool IsOnlineSerieBox { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("BoxId")]
        public virtual Box Box { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
        public virtual OnlineSerieBox OnlineSerieBox { get; set; }
    }
}