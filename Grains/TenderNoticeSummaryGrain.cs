using GocTenderNotices.Contracts.State;
using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using System.Text.RegularExpressions;

namespace Grains
{
    public class TenderNoticeSummaryGrain : Grain, ITenderNoticeSummary
    {
        private readonly ILogger<TenderNoticeSummaryGrain> _logger;
        private readonly IPersistentState<List<TenderNoticeState>> _state;

        public TenderNoticeSummaryGrain(
         ILogger<TenderNoticeSummaryGrain> logger,
            [PersistentState("tenderNoticeSummary", "tenderNoticesSummaryStore")] IPersistentState<List<TenderNoticeState>> state)
        {
            _logger = logger;
            _state = state;
        }

        public async Task ProcessUpdate(TenderNoticeState state, bool remove)
        {
            var matches = _state.State.Where(id => id.FeedGuid.Equals(state.FeedGuid, StringComparison.OrdinalIgnoreCase)).ToList();

            if (matches.Any())
            {
                if (remove)
                {
                    matches.ForEach(match => _state.State.Remove(match));
                } 
                else
                {
                    matches.ForEach(match => {
                        match.UpdatedDate = DateTime.Now;
                        match.Creator = state.Creator ?? match.Creator;
                        match.Description = state.Description ?? match.Description;
                        match.Link = state.Link ?? match.Link;
                    });
                }
            } 
            else
            {
                // Asked to remove a record. It doesn't exist. Return early.
                if (remove) {
                    return;
                }
                _state.State.Add(state);
            }
            await _state.WriteStateAsync();
        }
    }
}
