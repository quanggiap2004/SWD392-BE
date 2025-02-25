using System.ComponentModel.DataAnnotations;

namespace BlindBoxSystem.Domain.Entities
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public virtual ICollection<OrderStatusDetail> OrderStatusDetail { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}