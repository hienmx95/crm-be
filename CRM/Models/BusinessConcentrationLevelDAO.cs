using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class BusinessConcentrationLevelDAO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Branch { get; set; }
        public decimal? RevenueInYear { get; set; }
        public long? MarketingStaff { get; set; }
        public long? StoreId { get; set; }

        public virtual StoreDAO Store { get; set; }
    }
}
