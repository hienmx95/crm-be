using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_BusinessConcentrationLevelDTO : DataDTO
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Branch { get; set; }
        public decimal? RevenueInYear { get; set; }
        public long? MarketingStaff { get; set; }
        public long? StoreId { get; set; }

        public Store_BusinessConcentrationLevelDTO()
        {

        }
        public Store_BusinessConcentrationLevelDTO(BusinessConcentrationLevel BusinessConcentrationLevel)
        {
            this.Id = BusinessConcentrationLevel.Id;
            this.Name = BusinessConcentrationLevel.Name;
            this.Manufacturer = BusinessConcentrationLevel.Manufacturer;
            this.Branch = BusinessConcentrationLevel.Branch;
            this.RevenueInYear = BusinessConcentrationLevel.RevenueInYear;
            this.MarketingStaff = BusinessConcentrationLevel.MarketingStaff;
            this.StoreId = BusinessConcentrationLevel.StoreId;
        }

    }

}
