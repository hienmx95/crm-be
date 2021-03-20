using CRM.Common;
using CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.kpi_item
{
    public class KpiItem_ImportDTO : DataDTO
    {
        public long Stt { get; set; }
        public string UsernameValue { get; set; }
        public string ItemCodeValue { get; set; }
        public string OrderOutput { get; set; }
        public string Sales { get; set; }
        public string OrderNumber { get; set; }
        public string NumberOfCustomer { get; set; } 
        public bool HasValue
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ItemCodeValue) || 
                    !string.IsNullOrWhiteSpace(OrderOutput) ||
                    !string.IsNullOrWhiteSpace(Sales) ||
                    !string.IsNullOrWhiteSpace(OrderNumber) ||
                    !string.IsNullOrWhiteSpace(NumberOfCustomer))
                    return true;
                return false;
            }
        }

        public bool IsNew { get; set; }
        public long EmployeeId { get; set; }
        public long ItemId { get; set; }
        public long KpiYearId { get; set; }
        public long KpiPeriodId { get; set; }
        public long OrganizationId { get; set; }
    }

    public class KpiItem_RowDTO : IEquatable<KpiItem_RowDTO>
    {
        public long AppUserId { get; set; }
        public long KpiYearId { get; set; }
        public long KpiPeriodId { get; set; }
        public bool Equals(KpiItem_RowDTO other)
        {
            return other != null && this.AppUserId == other.AppUserId && this.KpiYearId == other.KpiYearId && this.KpiPeriodId == other.KpiPeriodId;
        }
        public override int GetHashCode()
        {
            return AppUserId.GetHashCode() ^ KpiYearId.GetHashCode() ^ KpiPeriodId.GetHashCode();
        }
    }
}
