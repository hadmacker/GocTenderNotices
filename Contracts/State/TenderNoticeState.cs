namespace GocTenderNotices.Contracts.State
{
    public class TenderNoticeState
    {
        public string FeedGuid { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Flags { get; set; } = new();
        public DateTime VisibleDate { get; set; }
        public DateTime AmendedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Creator { get; set; }
        public ProcurementStatus Status { get; set; }
    }
}