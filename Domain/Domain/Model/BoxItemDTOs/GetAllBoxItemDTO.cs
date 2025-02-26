﻿using Domain.Domain.Model.BoxDTOs;

namespace Domain.Domain.Model.BoxItemDTOs
{
    public class GetAllBoxItemDTO
    {
        public int BoxItemId { get; set; }
        public string BoxItemName { get; set; }
        public string BoxItemDescription { get; set; }
        public string BoxItemEyes { get; set; }
        public string BoxItemColor { get; set; }
        public int AverageRating { get; set; }
        public string ImageUrl { get; set; }
        public int NumOfVote { get; set; }
        public bool IsSecret { get; set; }
        public ICollection<VotedResponseDTO> VotedResponse { get; set; }
        public BelongBoxResponseDTO BelongBox { get; set; }

    }
}
