using GocTenderNotices.Contracts.State;
using GrainInterfaces;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Extensions.Logging;
using Orleans.Providers;
using Orleans.Runtime;

namespace Grains
{
    public class TenderNoticeGrain : Grain, ITenderNotice
    {
        private readonly IPersistentState<TenderNoticeState> _state;

        private readonly ILogger _logger;
        public TenderNoticeGrain(
            ILogger<TenderNoticeGrain> logger, 
            [PersistentState("tenderNotice", "tenderNoticesStore")] IPersistentState<TenderNoticeState> tenderNoticeState)
        {
            _logger = logger;
            _state = tenderNoticeState;
        }

        public Task<string> GetNameAsync() => Task.FromResult(_state.State.FeedGuid);

        public async Task ProcessUpdate(TenderNoticeState state, ProcurementStatus status)
        {
            _logger.LogInformation($"ProcessUpdate for {state.FeedGuid}, Status: {status}");
            _state.State = state;
            await _state.WriteStateAsync();
        }
    }
}
