using System.ComponentModel.DataAnnotations;

namespace Common.Model.OrderDTOs.Request
{
    public class GetAllOrderRequestDto
    {
        public int userId { get; set; }

        [Required(ErrorMessage = "Please enter the status Pending, Cancelled, Shipping, Arrived, Processing")]
        public ICollection<string> orderStatusList { get; set; }
    }
}
