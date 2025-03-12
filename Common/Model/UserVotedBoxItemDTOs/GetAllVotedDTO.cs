namespace Common.Model.UserVotedBoxItemDTOs
{
    public class GetAllVotedDTO
    {
        public int UserVotedBoxItemId { get; set; }
        public int BoxItemId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public DateTime LastUpdated { get; set; }
        public int Rating { get; set; }
    }
}
