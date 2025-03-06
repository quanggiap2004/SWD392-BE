using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class OnlineSerieBox
    {
        [Key, ForeignKey("BoxOption")]
        public int OnlineSerieBoxId { get; set; }
        public decimal PriceAfterSecret { get; set; }
        public int PriceIncreasePercent { get; set; }
        public string Name { get; set; }
        public bool IsSecretOpen { get; set; } = false;
        public int Turn { get; set; }
        public int MaxTurn { get; set; }
        public virtual BoxOption BoxOption { get; set; }
        public bool IsPublished { get; set; } = false;
    }
}