using GocTenderNotices.Contracts.State;
using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains
{
    public class TenderNoticeGrain : Grain, ITenderNotice
    {
        private readonly ILogger _logger;
        public TenderNoticeGrain(ILogger<TenderNoticeGrain> logger) => _logger = logger;
        public Task ProcessUpdate(TenderNoticeState state, ProcurementStatus status)
        {
            _logger.LogInformation($"ProcessUpdate for {state.FeedGuid}, Status: {status}");
            return Task.CompletedTask;
        }
    }
}
