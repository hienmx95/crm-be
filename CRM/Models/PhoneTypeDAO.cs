﻿using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class PhoneTypeDAO
    {
        public PhoneTypeDAO()
        {
            CustomerPhones = new HashSet<CustomerPhoneDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CustomerPhoneDAO> CustomerPhones { get; set; }
    }
}