using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class CurrentRolledItem
    {
        [Key]
        public int CurrentRolledItemId { get; set; }
        public int OnlineSerieBoxId { get; set; }
        public int BoxItemId { get; set; }
        public bool IsDisable { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual OnlineSerieBox OnlineSerieBox { get; set; }
        public virtual BoxItem BoxItem { get; set; }
    }
}
