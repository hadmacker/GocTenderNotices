namespace WebApi.DTO
{
    [Serializable]
    public class TenderNoticeDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime VisibleDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
    }
}
