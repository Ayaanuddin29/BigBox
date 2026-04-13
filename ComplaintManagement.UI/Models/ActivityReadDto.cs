namespace ComplaintManagement.UI.Models
{
    public class ActivityReadDto
    {
        public int incid_no { get; set; }
        public int activity_id { get; set; }
        public string Summary { get; set; }
        public string Rec_created { get; set; }
        public string? Details { get; set; }
        public List<ActivityAttachmentDto> Attachments { get; set; }
    }
    public class ActivityAttachmentDto
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }   // image bytes
        
    }

}
