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

            //Voucher
            CreateMap<CreateVoucherRequest, Voucher>();
            CreateMap<Voucher, VoucherResponseDto>();
            CreateMap<UpdateVoucherRequest, Voucher>();

            //Feedback
            CreateMap<FeedbackRequestDto, Feedback>();
            CreateMap<Feedback, FeedbackResponseDto>();
            CreateMap<Feedback, FullFeedbackResponseDto>()
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.User.UserId))
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.User.Email));

            //BoxItem
            CreateMap<BoxItem, BoxItemResponseDto>();
        }
    }
}
