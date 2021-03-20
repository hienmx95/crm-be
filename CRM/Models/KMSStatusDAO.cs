using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class KMSStatusDAO
    {
        public KMSStatusDAO()
        {
            KnowledgeArticles = new HashSet<KnowledgeArticleDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<KnowledgeArticleDAO> KnowledgeArticles { get; set; }
    }
}
