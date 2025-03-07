namespace Common.Model.OnlineSerieBoxDTOs.Response
{
    public class BoxOptionResponse
    {
        public int boxOptionId { get; set; }
        public string boxOptionName { get; set; }
        public decimal originPrice { get; set; }
        public decimal displayPrice { get; set; }
        public int boxOptionStock { get; set; }
    }
}
