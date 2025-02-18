namespace BlindBoxSystem.Domain.Model.BoxOptionDTOs
{
    public class AddBoxOptionDTO
    {
        public string BoxOptionName { get; set; }  // nvarchar(200)
        public float BoxOptionPrice { get; set; }  // float
        public int BoxOptionStock { get; set; }  // int
        public float OriginPrice { get; set; }  // float
        public float DisplayPrice { get; set; }  // float
        public bool IsDeleted { get; set; } = false;
        public int BoxId { get; set; }  // Foreign Key to Box


    }
}
