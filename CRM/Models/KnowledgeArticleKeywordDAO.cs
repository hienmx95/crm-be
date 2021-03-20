using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class KnowledgeArticleKeywordDAO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long KnowledgeArticleId { get; set; }

        public virtual KnowledgeArticleDAO KnowledgeArticle { get; set; }
    }
}
