using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Model.BlogPostDTOs
{
    public class AuthorDTO
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Fullname { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
