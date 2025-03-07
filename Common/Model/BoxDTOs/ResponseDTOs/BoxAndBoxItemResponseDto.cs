using Common.Model.BoxItemDTOs.Response;

namespace Common.Model.BoxDTOs.ResponseDTOs
{
    public class BoxAndBoxItemResponseDto
    {
        public int boxId { get; set; }
        public string boxName { get; set; }

        public string? boxDescription { get; set; }
        public bool isDeleted { get; set; }
        public int soldQuantity { get; set; }
        public int brandId { get; set; }
        public ICollection<BoxItemResponseDto> boxItems { get; set; }
    }
}
