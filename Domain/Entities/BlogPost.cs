using System.ComponentModel.DataAnnotations;

namespace BlindBoxSystem.Domain.Entities
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }

        public string BlogPostTitle { get; set; }

        public string BlogPostContent { get; set; }

        public string BlogPostImage { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
