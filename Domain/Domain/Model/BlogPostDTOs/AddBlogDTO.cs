namespace Domain.Domain.Model.BlogPostDTOs
{
    public class AddBlogDTO
    {
        public string BlogPostTitle { get; set; }

        public string BlogPostContent { get; set; }

        public string BlogPostImage { get; set; }
        public int UserId { get; set; }

    }
}
