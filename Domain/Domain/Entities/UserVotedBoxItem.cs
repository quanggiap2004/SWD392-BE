using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class UserVotedBoxItem
    {
        [Key]
        public int UserVotedBoxItemId { get; set; }
        public int BoxItemId { get; set; }
        public int UserId { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Rating { get; set; }

        [ForeignKey("BoxItemId")]
        public virtual BoxItem BoxItem { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}