using GocTenderNotices.Contracts.State;

namespace GrainInterfaces
{
    public interface ITenderNoticeSummary : IGrainWithStringKey
    {
        Task ProcessUpdate(TenderNoticeState state, bool remove);
    }
}
