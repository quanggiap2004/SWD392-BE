using Domain.Domain.Model.BoxOptionDTOs.Request;
using Domain.Domain.Model.BoxOptionDTOs.Response;
namespace Domain.Domain.Model.OnlineSerieBoxDTOs.Response
{
    public class CreateBoxOptionAndOnlineSerieBoxResponse
    {
        public bool isSecretOpen { get; set; }
        public int maxTurn { get; set; }
        public string name { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public int turn { get; set; }
        public CreateBoxOptionResponse createBoxOptionResponse { get; set; }
    }
}
