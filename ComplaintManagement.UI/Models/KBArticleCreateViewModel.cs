using System.ComponentModel.DataAnnotations;

namespace ComplaintManagement.UI.Models
{
    public class KBArticleCreateViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        public string? Summary { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string? Content { get; set; }

        public int ArticleNumber { get; set; }

        public int? CategoryId { get; set; }

        public int? StatusId { get; set; }

        public List<IFormFile>? UploadFiles { get; set; }

        // ⭐ ADD THIS PROPERTY
        public string? FilePath { get; set; }

        public string? RelatedUrl { get; set; }

        public bool IsGeneralArticle { get; set; }

        public bool IsTechnicalArticle { get; set; }

        public bool IsFeatured { get; set; }

        public string? LoginCreated { get; set; }
    }
}