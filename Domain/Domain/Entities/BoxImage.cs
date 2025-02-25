using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class BoxImage
    {
        [Key]
        public int BoxImageId { get; set; }
        public string BoxImageUrl { get; set; }
        public int BoxId { get; set; }
        [ForeignKey("BoxId")]
        public virtual Box Box { get; set; }
    }
}
