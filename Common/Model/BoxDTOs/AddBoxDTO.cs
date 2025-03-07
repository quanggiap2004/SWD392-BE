namespace Common.Model.BoxDTOs
{
    public class AddBoxDTO
    {
        public string BoxName { get; set; }
        public string? BoxDescription { get; set; }
        // Foreign Key to Brand
        public int BrandId { get; set; }
    }
}
