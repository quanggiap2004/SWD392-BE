namespace Common.Model.BoxOptionDTOs.Request
{
    public class AddBoxOptionDTO
    {
        public string BoxOptionName { get; set; }  // nvarchar(200)
        public int BoxOptionStock { get; set; }  // int
        public decimal OriginPrice { get; set; }  // float
        public decimal DisplayPrice { get; set; }  // float
        public bool IsDeleted { get; set; } = false;
        public int BoxId { get; set; }  // Foreign Key to Box
        public bool isOnlineSerieBox { get; set; }
    }
}
