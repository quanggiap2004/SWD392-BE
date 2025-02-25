using BlindBoxSystem.Domain.Model.BoxDTOs;

namespace BlindBoxSystem.Domain.Model.BoxImageDTOs
{
    public class GetAllBoxImageDTO
    {
        public int BoxImageId { get; set; }
        public string BoxImageUrl { get; set; }
        public BelongBoxResponseDTO BelongBox { get; set; }
    }
}
