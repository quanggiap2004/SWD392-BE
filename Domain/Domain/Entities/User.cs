using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public bool Gender { get; set; }
        public bool IsActive { get; set; } = true;

        public int RoleId { get; set; }
        public string? AvatarUrl {  get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<UserVotedBoxItem> UserVotedBoxItems { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; }

    }
}
