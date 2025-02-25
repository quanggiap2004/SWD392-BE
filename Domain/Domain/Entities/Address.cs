using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetail { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}