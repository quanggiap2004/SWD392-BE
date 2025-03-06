using Domain.Domain.Model.BoxImageDTOs;
using Domain.Domain.Model.BoxItemDTOs;
using Domain.Domain.Model.BoxOptionDTOs;
using Domain.Domain.Model.OnlineSerieBoxDTOs;

namespace Domain.Domain.Model.BoxDTOs
{
    public class GetAllBoxesDTO
    {
        public int BoxId { get; set; } // Primary Key

        public string BoxName { get; set; }

        public string? BoxDescription { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int SoldQuantity { get; set; }

        // Foreign Key to Brand
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        // Navigation Properties (if Box has relationships)
        public List<BoxImageDTO> BoxImage { get; set; }
        public List<BoxItemDTO> BoxItem { get; set; }
        public List<BoxOptionDTO> BoxOptions { get; set; }
    }
}
