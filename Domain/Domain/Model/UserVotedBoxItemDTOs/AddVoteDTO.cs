namespace Domain.Domain.Model.UserVotedBoxItemDTOs
{
    public class AddVoteDTO
    {
        public int BoxItemId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }

    }
}
