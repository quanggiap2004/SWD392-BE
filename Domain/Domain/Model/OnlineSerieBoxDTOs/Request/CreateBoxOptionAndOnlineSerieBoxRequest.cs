using Domain.Domain.Model.BoxOptionDTOs.Request;

namespace Domain.Domain.Model.OnlineSerieBoxDTOs.Request
{
    public class CreateBoxOptionAndOnlineSerieBoxRequest
    {
        public string name { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public CreateBoxOptionRequest createBoxOptionRequest {get; set; }
    }
}
