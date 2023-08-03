namespace WebApi.Contracts.DTO
{
    [Serializable]
    public class TenderNoticeDto
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Link { get; set; }
        public required string Status { get; set; }
        public string? Description { get; set; }
        public DateTime? VisibleDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Creator { get; set; }
    }
}
