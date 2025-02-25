using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Domain.Entities
{
    [PrimaryKey(nameof(OrderId), nameof(OrderStatusId))]
    public class OrderStatusDetail
    {
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime OrderStatusUpdatedAt { get; set; }

        public string? OrderStatusNote { get; set; }
        public virtual Order Order { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}