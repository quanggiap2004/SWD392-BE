namespace Domain.Domain.Model.BoxItemDTOs
{
    public class BoxItemDTO
    {
        public int BoxItemId { get; set; }

        public string BoxItemName { get; set; }

        public string BoxItemDescription { get; set; }

        public string BoxItemEyes { get; set; }

        public string BoxItemColor { get; set; }

        public int AverageRating { get; set; }

        public int BoxId { get; set; }

        public string ImageUrl { get; set; }

        public int NumOfVote { get; set; }

        public bool IsSecret { get; set; }

    }
}
