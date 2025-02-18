namespace BlindBoxSystem.Domain.Model.BoxItemDTOs
{
    public class AddBoxItemDTO
    {
        public string BoxItemName { get; set; }
        public string BoxItemDescription { get; set; }
        public string BoxItemEyes { get; set; }
        public string BoxItemColor { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSecret { get; set; }
        public int BoxId { get; set; }
    }
}
