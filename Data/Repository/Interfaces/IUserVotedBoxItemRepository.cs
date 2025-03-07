using Common.Model.UserVotedBoxItemDTOs;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IUserVotedBoxItemRepository

    {
        Task<IEnumerable<UserVotedBoxItem>> GetAllVoted();
        Task<UserVotedBoxItem> AddOrUpdateVoteAsync(AddVoteDTO addVoteDTO);
        Task<IEnumerable<UserVotedBoxItem>> GetVotesByBoxItemIdAsync(int boxItemId);
    }
}
