using Common.Model.BoxOptionDTOs.Response;

namespace Common.Model.OnlineSerieBoxDTOs.Response
{
    public class CreateBoxOptionAndOnlineSerieBoxResponse
    {
        public bool isSecretOpen { get; set; }
        public int maxTurn { get; set; }
        public string name { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public decimal basePrice { get; set; }
        public int turn { get; set; }
        public string imageUrl { get; set; }
        public CreateBoxOptionResponse createBoxOptionResponse { get; set; }
    }
}
