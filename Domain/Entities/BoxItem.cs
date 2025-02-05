using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlindBoxSystem.Domain.Entities
{
    public class BoxItem
    {
        [Key]
        public int BoxItemId { get; set; }

        [Required]
        public string BoxItemName { get; set; }

        public string BoxItemDescription { get; set; }

        public string BoxItemEyes { get; set; }

        public string BoxItemColor { get; set; }

        public int AverageRating { get; set; }

        public int BoxId { get; set; }

        public string ImageUrl { get; set; }

        public int NumOfVote { get; set; }

        public bool IsSecret { get; set; }

        [ForeignKey("BoxId")]
        public virtual Box Box { get; set; }
        public virtual ICollection<UserVotedBoxItem> UserVotedBoxItems { get; set; }
        public virtual ICollection<OnlineSerieBox> OnlineSerieBoxes { get; set; }

    }
}