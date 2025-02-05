using System.ComponentModel.DataAnnotations;

namespace BlindBoxSystem.Domain.Entities
{
    public class Variant
    {
        [Key]
        public int VariantId { get; set; }
        public string VariantName { get; set; }
        public virtual ICollection<BoxVariant> BoxVariants { get; set; }
        public virtual ICollection<Box> Boxes { get; set; }
    }
}