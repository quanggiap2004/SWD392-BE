namespace Common.Model.OnlineSerieBoxDTOs.Response
{
    public class UpdateOnlineSerieBoxResponse
    {
        public int onlineSerieBoxId { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public decimal displayPrice { get; set; }
        public decimal originPrice { get; set; }
        public decimal basePrice { get; set; }
        public bool isPublished { get; set; }
        public string imageUrl { get; set; }
    }
}
