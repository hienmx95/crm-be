﻿using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAAlertMailDAO
    {
        public long Id { get; set; }
        public long? SLAAlertId { get; set; }
        public string Mail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual SLAAlertDAO SLAAlert { get; set; }
    }
}
