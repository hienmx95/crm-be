using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderProjectContactMappingDAO
    {
        public long OrderProjectId { get; set; }
        public long ContactId { get; set; }

        public virtual ContactDAO Contact { get; set; }
        public virtual OrderProjectDAO OrderProject { get; set; }
    }
}
