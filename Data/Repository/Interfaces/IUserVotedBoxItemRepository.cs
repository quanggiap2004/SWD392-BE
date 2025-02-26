using Domain.Domain.Entities;
using Domain.Domain.Model.UserVotedBoxItemDTOs;

namespace Data.Repository.Interfaces
{
    public interface IUserVotedBoxItemRepository

    {
        Task<IEnumerable<UserVotedBoxItem>> GetAllVoted();
        Task<UserVotedBoxItem> AddOrUpdateVoteAsync(AddVoteDTO addVoteDTO);
        Task<IEnumerable<UserVotedBoxItem>> GetVotesByBoxItemIdAsync(int boxItemId);
    }
}
