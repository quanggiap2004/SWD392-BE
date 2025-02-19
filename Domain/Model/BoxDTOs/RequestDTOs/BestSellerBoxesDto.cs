namespace BlindBoxSystem.Domain.Model.BoxDTOs.RequestDTOs
{
    public class BestSellerBoxesDto
    {
        public int boxId { get; set; }

        public string boxName { get; set; }

        public string? boxDescription { get; set; }
        public bool isDeleted { get; set; } = false;
        public int soldQuantity { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public IEnumerable<string> imageUrl { get; set; }
        public IEnumerable<int> boxOptionIds { get; set; }
        
    }
}
