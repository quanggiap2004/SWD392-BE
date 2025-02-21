using System.ComponentModel.DataAnnotations;

namespace BlindBoxSystem.Domain.Model.OrderDTOs.Request
{
    public class GetAllOrderRequestDto
    {
        public int userId { get; set; }

        [Required(ErrorMessage ="Please enter the status Pending, Cancelled, Shipping, Arrived, Processing")]
        public ICollection<string> orderStatusList { get; set; }
    }
}
