using Orleans;
namespace GocTenderNotices.Contracts.State
{
    [GenerateSerializer]
    public class TenderNoticeState
    {
        [Id(0)]
        public string FeedGuid { get; set; }
        [Id(1)]
        public string Title { get; set; }
        [Id(2)]
        public string Link { get; set; }
        [Id(3)]
        public string Description { get; set; }
        [Id(4)]
        public DateTime VisibleDate { get; set; }
        [Id(6)]
        public DateTime UpdatedDate { get; set; }
        [Id(7)]
        public string Creator { get; set; }
        [Id(8)]
        public Dictionary<string, string> Flags { get; set; } = new();
        [Id(9)]
        public ProcurementStatus Status { get; set; }
    }
}