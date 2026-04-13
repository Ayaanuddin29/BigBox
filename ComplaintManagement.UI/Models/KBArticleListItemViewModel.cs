namespace ComplaintManagement.UI.Models
{
    public class KBArticleListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string StatusName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}