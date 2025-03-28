﻿namespace Common.Model.BoxOptionDTOs
{
    public class BoxOptionDTO
    {
        public int BoxOptionId { get; set; }  // Primary Key
        public string BoxOptionName { get; set; }  // nvarchar(200)
        public decimal OriginPrice { get; set; }  // float
        public decimal DisplayPrice { get; set; }  // float
        public int BoxOptionStock { get; set; }  // int
        public bool IsDeleted { get; set; } = false;
        public bool IsOnlineSerieBox { get; set; } = false;

        public int BoxId { get; set; }  // Foreign Key to Box
    }
}
