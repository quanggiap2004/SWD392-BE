using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class UserRolledItem
    {
        [Key]
        public int UserRolledItemId { get; set; }
        public int OnlineSerieBoxId { get; set; }
        public int UserId { get; set; }
        public int BoxItemId { get; set; }
        public bool IsCheckOut { get; set; } = false;
        public virtual OnlineSerieBox OnlineSerieBox { get; set; }
        public virtual User User { get; set; }
        public virtual BoxItem BoxItem { get; set; }
        public virtual OrderItem OrderItem { get; set; }
    }
}
