using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlindBoxSystem.Domain.Entities
{
    public class OnlineSerieBox
    {
        [Key]
        public int OnlineSerieBoxId { get; set; }

        public int BoxId { get; set; }

        public float Price { get; set; }

        public string Name { get; set; }

        public bool IsSecretOpen { get; set; }

        public int Turn { get; set; }

        [ForeignKey("BoxId")]
        public virtual Box Box { get; set; }
        public virtual ICollection<BoxItem> BoxItems { get; set; }
    }
}