using Common.Model.UserRolledItemDTOs.Response;

namespace Common.Model.BoxItemDTOs.Response
{
    public class BoxItemResponseDto
    {
        public int boxItemId { get; set; }
        public string boxItemName { get; set; }

        public string boxItemDescription { get; set; }

        public string boxItemEyes { get; set; }

        public string boxItemColor { get; set; }

        public float averageRating { get; set; }

        public int boxId { get; set; }

        public string imageUrl { get; set; }

        public int numOfVote { get; set; }

        public bool isSecret { get; set; }

        public UserRolledItemForManageOrder? userRolledItem { get; set; }
    }
}
