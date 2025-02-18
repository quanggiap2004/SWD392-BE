namespace BlindBoxSystem.Domain.Model.BoxImageDTOs
{
    public class GetAllBoxImageDTO
    {
        public int BoxImageId { get; set; }
        public string BoxImageUrl { get; set; }
        public int BoxId { get; set; }
        public string BoxName { get; set; }
    }
}
