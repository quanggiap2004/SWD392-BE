namespace Common.Model.BoxOptionDTOs.Request
{
    public class CreateBoxOptionRequest
    {
        public int boxId { get; set; }
        public string boxOptionName { get; set; }
        public int boxOptionStock { get; set; }
        public decimal originPrice { get; set; }
        public decimal displayPrice { get; set; }
        public bool isOnlineSerieBox { get; set; } = true;
    }
}
