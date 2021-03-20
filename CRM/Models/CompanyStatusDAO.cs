using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CompanyStatusDAO
    {
        public CompanyStatusDAO()
        {
            Companies = new HashSet<CompanyDAO>();
        }

        public long Id { get; set; }
        /// <summary>
        /// Mã quận huyện
        /// </summary>
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompanyDAO> Companies { get; set; }
    }
}
