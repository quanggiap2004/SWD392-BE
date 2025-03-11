namespace Common.Model.OnlineSerieBoxDTOs.Request
{
    public class UpdateOnlineSerieBoxRequest
    {
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public decimal displayPrice { get; set; }
        public decimal originPrice { get; set; }
        public string imageUrl { get; set; }
    }
}
