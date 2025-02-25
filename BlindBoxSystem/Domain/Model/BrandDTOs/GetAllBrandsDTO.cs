namespace BlindBoxSystem.Domain.Model.BrandDTOs
{
    public class GetAllBrandsDTO
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public virtual List<String> IncludedBox { get; set; } = new List<String>();
    }
}
