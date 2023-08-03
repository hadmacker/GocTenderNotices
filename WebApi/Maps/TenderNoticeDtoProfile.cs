using AutoMapper;
using GocTenderNotices.Contracts.State;
using WebApi.Contracts.DTO;

namespace WebApi.Maps
{
    public class TenderNoticeDtoProfile : Profile
    {
        public TenderNoticeDtoProfile() {
            CreateMap<string, ProcurementStatus>().ConvertUsing(new StringToEnumConverter<ProcurementStatus>());

            CreateMap<TenderNoticeState, TenderNoticeDto>()
                .ForMember(src => src.Id, opt => opt.MapFrom(dest => dest.FeedGuid))
                .ReverseMap();
        }
    }
}
