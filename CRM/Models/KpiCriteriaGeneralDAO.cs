﻿using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class KpiCriteriaGeneralDAO
    {
        public KpiCriteriaGeneralDAO()
        {
            KpiGeneralContents = new HashSet<KpiGeneralContentDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<KpiGeneralContentDAO> KpiGeneralContents { get; set; }
    }
}
