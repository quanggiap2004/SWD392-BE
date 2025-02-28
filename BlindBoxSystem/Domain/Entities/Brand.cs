using System.ComponentModel.DataAnnotations;

namespace BlindBoxSystem.Domain.Entities
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string url
        public virtual ICollection<Box> Box { get; set; }
    }
}