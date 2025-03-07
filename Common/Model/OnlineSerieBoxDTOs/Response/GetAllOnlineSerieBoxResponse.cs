namespace Common.Model.OnlineSerieBoxDTOs.Response
{
    public class GetAllOnlineSerieBoxResponse
    {
        public int onlineSerieBoxId { get; set; }
        public string name { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public decimal basePrice { get; set; }
        public bool isPublished { get; set; }
        public int maxTurn { get; set; }
        public int turn { get; set; }
        public BoxOptionResponse boxOption { get; set; }
    }
}
