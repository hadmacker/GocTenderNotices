using AutoMapper;
using GocTenderNotices.Contracts.State;
using WebApi.DTO;

namespace WebApi.Maps
{
    public class TenderNoticeDtoProfile : Profile
    {
        public TenderNoticeDtoProfile() {
            CreateMap<TenderNoticeState, TenderNoticeDto>().ForMember(src => src.Id, opt => opt.MapFrom(desc => desc.FeedGuid)).ReverseMap();
        }
    }
}
