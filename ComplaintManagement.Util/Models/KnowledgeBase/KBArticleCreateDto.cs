using System;

namespace ComplaintManagement.Util.Models.KnowledgeBase
{
    public class KBArticleCreateDto
    {
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Content { get; set; }

        public int? CategoryId { get; set; }
        public int? StatusId { get; set; }

        public bool IsFeatured { get; set; }
        //public int? OwnerUserId { get; set; }
        public string? LoginCreated { get; set; }

        public string? FilePath { get; set; }

        public string? RelatedUrl { get; set; }

        public bool IsGeneralArticle { get; set; }

        public bool IsTechnicalArticle { get; set; }
    }
}