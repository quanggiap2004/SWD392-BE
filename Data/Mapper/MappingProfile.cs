using AutoMapper;
using Common.Model.BoxItemDTOs.Response;
using Common.Model.BoxOptionDTOs.Response;
using Common.Model.CurrentRolledITemDTOs.Request;
using Common.Model.OnlineSerieBoxDTOs.Response;
using Domain.Domain.Entities;

namespace Data.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BoxOption, CreateBoxOptionResponse>();
            CreateMap<BoxOption, BoxOptionResponse>();
            CreateMap<OnlineSerieBox, GetAllOnlineSerieBoxResponse>();
            CreateMap<CurrentRolledItemDto, CurrentRolledItem>();

            //BoxItem
            CreateMap<BoxItem, BoxItemResponseDto>();
        }
    }
}
