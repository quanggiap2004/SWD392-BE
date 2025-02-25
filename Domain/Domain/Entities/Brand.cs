using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public virtual ICollection<Box> Box { get; set; }
    }
}