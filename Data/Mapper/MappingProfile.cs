using AutoMapper;
using Common.Model.BoxItemDTOs.Response;
using Common.Model.BoxOptionDTOs.Response;
using Common.Model.CurrentRolledITemDTOs.Request;
using Common.Model.FeedbackDTOs.Request;
using Common.Model.FeedbackDTOs.Response;
using Common.Model.OnlineSerieBoxDTOs.Response;
using Common.Model.VoucherDTOs.Request;
using Common.Model.VoucherDTOs.Response;
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
