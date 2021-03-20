using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerFeedbackTypeDAO
    {
        public CustomerFeedbackTypeDAO()
        {
            CustomerFeedbacks = new HashSet<CustomerFeedbackDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerFeedbackDAO> CustomerFeedbacks { get; set; }
    }
}
