using Common.Model.BoxOptionDTOs.Request;

namespace Common.Model.OnlineSerieBoxDTOs.Request
{
    public class CreateBoxOptionAndOnlineSerieBoxRequest
    {
        public string name { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public string imageUrl { get; set; }
        public CreateBoxOptionRequest createBoxOptionRequest { get; set; }
    }
}
