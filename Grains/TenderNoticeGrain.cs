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

            var tasks = new List<Task>();

            var processActive = (bool remove) =>
            {
                var summaryGrain = GrainFactory.GetGrain<ITenderNoticeSummary>(ProcurementStatus.Active.ToString());
                tasks.Add(summaryGrain.ProcessUpdate(state, remove));
            };
            var processAmended = (bool remove) =>
            {
                var summaryGrain = GrainFactory.GetGrain<ITenderNoticeSummary>(ProcurementStatus.Amended.ToString());
                tasks.Add(summaryGrain.ProcessUpdate(state, remove));
            };
            var processExpired = (bool remove) =>
            {
                var summaryGrain = GrainFactory.GetGrain<ITenderNoticeSummary>(ProcurementStatus.Expired.ToString());
                tasks.Add(summaryGrain.ProcessUpdate(state, remove));
            };
            var processAwarded = (bool remove) =>
            {
                var summaryGrain = GrainFactory.GetGrain<ITenderNoticeSummary>(ProcurementStatus.Awarded.ToString());
                tasks.Add(summaryGrain.ProcessUpdate(state, remove));
            };

            switch (state.Status)
            {
                case ProcurementStatus.Active:
                    processActive(false);
                    break;
                case ProcurementStatus.Awarded:
                    processActive(true);
                    processAwarded(false);
                    break;
                case ProcurementStatus.Expired:
                    processActive(true);
                    processAwarded(true);
                    processExpired(false);
                    break;
                case ProcurementStatus.Amended:
                    processActive(false);
                    processAmended(false);
                    processAwarded(false);
                    break;
            }

            _state.State = state;
            tasks.Add(_state.WriteStateAsync());

            await Task.WhenAll(tasks);
        }
    }
}
