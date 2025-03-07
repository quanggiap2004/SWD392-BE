namespace Common.Model.BlogPostDTOs
{
    public class GetAllBlogDTO
    {
        public int BlogPostId { get; set; }
        public string BlogPostTitle { get; set; }
        public string BlogPostContent { get; set; }
        public string BlogPostImage { get; set; }
        public DateTime BlogCreatedDate { get; set; }
        public AuthorDTO Author { get; set; }

    }
}
