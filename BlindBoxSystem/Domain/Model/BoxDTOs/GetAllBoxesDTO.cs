﻿using BlindBoxSystem.Domain.Model.BoxImageDTOs;
using BlindBoxSystem.Domain.Model.BoxItemDTOs;
using BlindBoxSystem.Domain.Model.BoxOptionDTOs;
using BlindBoxSystem.Domain.Model.OnlineSerieBoxDTOs;

namespace BlindBoxSystem.Domain.Model.BoxDTOs
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
        public List<OnlineSerieBoxDTO> OnlineSerieBox { get; set; }
    }
}
