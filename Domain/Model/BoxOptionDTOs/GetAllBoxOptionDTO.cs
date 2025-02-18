using BlindBoxSystem.Domain.Model.BoxDTOs;

namespace BlindBoxSystem.Domain.Model.BoxOptionDTOs
{
    public class GetAllBoxOptionDTO
    {

        public int BoxOptionId { get; set; }  // Primary Key
        public string BoxOptionName { get; set; }  // nvarchar(200)
        public float BoxOptionPrice { get; set; }  // float
        public float OriginPrice { get; set; }  // float
        public float DisplayPrice { get; set; }  // float
        public int BoxOptionStock { get; set; }  // int
        public bool IsDeleted { get; set; } = false;
        public BelongBoxResponseDTO BelongBox { get; set; }

    }
}
