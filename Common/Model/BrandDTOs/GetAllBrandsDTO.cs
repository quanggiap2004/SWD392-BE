namespace Common.Model.BrandDTOs
{
    public class GetAllBrandsDTO
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public virtual List<string> IncludedBox { get; set; } = new List<string>();
        public string? imageUrl { get; set; }
    }
}
