using Domain.Domain.Entities;
using Domain.Domain.Model.BoxItemDTOs;
using Domain.Domain.Model.UserVotedBoxItemDTOs;

namespace Application.Services.Interfaces
{
    public interface IBoxItemService
    {
        Task<IEnumerable<GetAllBoxItemDTO>> GetAllBoxItems();
        Task<BoxItem> GetBoxItemById(int id);
        Task<GetAllBoxItemDTO> GetBoxItemDTO(int id);
        Task<BoxItem> AddBoxItemAsync(BoxItem boxItem);
        Task<BoxItem> UpdateBoxItemAsync(int id, BoxItem boxItem);
        Task DeleteBoxItemAsync(int id);
        Task<GetAllVotedDTO> AddOrUpdateVoteAsync(AddVoteDTO addVoteDTO);
        Task<IEnumerable<UserVotedBoxItem>> GetVotesByBoxItemId(int id);
    }
}
