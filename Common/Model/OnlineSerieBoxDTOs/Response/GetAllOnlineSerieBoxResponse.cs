﻿using Common.Model.BrandDTOs.Response;

namespace Common.Model.OnlineSerieBoxDTOs.Response
{
    public class GetAllOnlineSerieBoxResponse
    {
        public int onlineSerieBoxId { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public decimal basePrice { get; set; }
        public bool isPublished { get; set; }
        public int maxTurn { get; set; }
        public int turn { get; set; }
        public string imageUrl { get; set; }
        public bool isSecretOpen { get; set; }
        public BrandDtoResponse brandDtoResponse { get; set; }
        public BoxOptionResponse boxOption { get; set; }
    }
}
