using GocTenderNotices.Contracts.State;
using WebApi.Contracts.DTO;

namespace WebApi.Client
{
    public interface ITenderNoticesApiClient
    {
        Task<IEnumerable<TenderNoticeDto>> GetActiveAsync();
        Task<TenderNoticeDto> PostTenderNotice(TenderNoticeDto tenderNoticeDto);
    }
}