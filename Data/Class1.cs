using AutoMapper;
using Domain.Domain.Entities;
using Domain.Domain.Model.BoxOptionDTOs.Response;

namespace Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BoxOption, CreateBoxOptionResponse>();
        }
    }
}
