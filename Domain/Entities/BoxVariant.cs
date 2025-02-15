using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlindBoxSystem.Domain.Entities
{
    public class BoxVariant
    {
        [Key]
        public int BoxVariantId { get; set; }  // Primary Key

        
        public int VariantId { get; set; }  // Foreign Key to Variant

        
        public int BoxId { get; set; }  // Foreign Key to Box

        [Required]
        public string BoxVariantName { get; set; }  // nvarchar(200)

        public float BoxVariantPrice { get; set; }  // float

        public float OriginPrice { get; set; }  // float

        public float DisplayPrice { get; set; }  // float

        public int BoxVariantStock { get; set; }  // int
        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        [ForeignKey("VariantId")]
        public virtual Variant Variant { get; set; }
        [ForeignKey("BoxId")]
        public virtual Box Box { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}